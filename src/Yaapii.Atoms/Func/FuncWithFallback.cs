﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <typeparam name="In"></typeparam>
        /// <typeparam name="Out"></typeparam>
    public sealed class FuncWithFallback<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// func to call
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// fallback to call when necessary
        /// </summary>
        private readonly IFunc<Exception, Out> _fallback;

        /// <summary>
        /// a followup function
        /// </summary>
        private readonly IFunc<Out, Out> _follow;

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        public FuncWithFallback(System.Func<In, Out> func, System.Func<Exception, Out> fallback) :
            this(new FuncOf<In, Out>((X) => func(X)), new FuncOf<Exception, Out>((e) => fallback(e)))
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="func">func to call</param>
        /// <param name="fallback">fallback func</param>
        public FuncWithFallback(System.Func<In, Out> func, Atoms.IFunc<Exception, Out> fallback) :
            this(new FuncOf<In, Out>((X) => func(X)), fallback)
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">func to call</param>
        /// <param name="fbk">fallback func</param>
        public FuncWithFallback(IFunc<In, Out> fnc, IFunc<Exception, Out> fbk) :
        this(fnc, fbk, new FuncOf<Out, Out>((input) => input))
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">func to call</param>
        /// <param name="fbk">fallback func</param>
        /// <param name="flw">func to call afterwards</param>
        public FuncWithFallback(System.Func<In, Out> func, System.Func<Exception, Out> fallback, System.Func<Out, Out> flw) :
            this(new FuncOf<In, Out>((X) => func(X)), new FuncOf<Exception, Out>((e) => fallback(e)), new FuncOf<Out, Out>(flw))
        { }

        /// <summary>
        /// A function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">func to call</param>
        /// <param name="fbk">fallback func</param>
        /// <param name="flw">func to call afterwards</param>
        public FuncWithFallback(IFunc<In, Out> fnc, IFunc<Exception, Out> fbk, IFunc<Out, Out> flw)
        {
            this._func = fnc;
            this._fallback = fbk;
            this._follow = flw;
        }

        public Out Invoke(In input)
        {
            Out result;
            try
            {
                result = this._func.Invoke(input);
            }
            catch (System.Threading.ThreadStateException ex)
            {
                Thread.CurrentThread.Join();
                result = this._fallback.Invoke(ex);
            }
            catch (Exception ex)
            {
                result = this._fallback.Invoke(ex);
            }
            return this._follow.Invoke(result);
        }

    }
}
