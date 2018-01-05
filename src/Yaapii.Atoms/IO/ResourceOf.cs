using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Scalar;
using Yaapii.IO.Error;

namespace Yaapii.IO
{
    /// <summary>
    /// <para>A embedded resource.</para>
    /// </summary>
    public class ResourceOf : IInput
    {
        private readonly IScalar<string> _resourceName;
        private readonly IScalar<Assembly> _container;

        /// <summary>
        /// <para>A resource embedded in the container.</para>
        /// <para>Name must be provided as a relative path:</para>
        /// <para>If the resource is in project root path project\resource.txt, address it with resource.txt</para>
        /// <para>If the resource is in a subpath project\resources\resource.txt, address it with resources\resource.txt</para>
        /// <para>Please note that the the path is case sensitive.</para>
        /// </summary>
        /// <param name="name">name of the resource</param>
        /// <param name="type">a class that is in the same container (assembly) with the resource</param>
        public ResourceOf(string name, Type type) : this(
            name,
            new ScalarOf<Assembly>(Assembly.GetAssembly(type))
        )
        { }

        /// <summary>
        /// <para>A resource embedded in the container.</para>
        /// <para>Name must be provided as a relative path:</para>
        /// <para>If the resource is in project root path project\resource.txt, address it with resource.txt</para>
        /// <para>If the resource is in a subpath project\resources\resource.txt, address it with resources\resource.txt</para>
        /// <para>Please note that the the path is case sensitive.</para>
        /// </summary>
        /// <param name="name">name of the resource</param>
        /// <param name="container">container to search in. Use Assembly.GetExecutingAssembly() for the assembly your current code is in.</param>
        public ResourceOf(string name, Assembly container) : this(
            name,
            new ScalarOf<Assembly>(container)
        )
        { }

        /// <summary>
        /// <para>A resource embedded in the container.</para>
        /// <para>Name must be provided as a relative path:</para>
        /// <para>If the resource is in project root path project\resource.txt, address it with resource.txt</para>
        /// <para>If the resource is in a subpath project\resources\resource.txt, address it with resources\resource.txt</para>
        /// <para>Please note that the the path is case sensitive.</para>
        /// </summary>
        /// <param name="name">name of the resource</param>
        /// <param name="container">container to search in. Use Assembly.GetExecutingAssembly() for the assembly your current code is in.</param>
        public ResourceOf(string name, IScalar<Assembly> container) : this(
            new ScalarOf<string>(() => container.Value().GetName().Name + "." + name.Replace('/', '.').Replace('\\', '.')),
            container
            )
        { }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="container"></param>
        private ResourceOf(IScalar<string> name, IScalar<Assembly> container)
        {
            this._container = container;
            this._resourceName = name;
        }

        /// <summary>
        /// Stream of the resource.
        /// </summary>
        /// <exception cref = "ResourceNotFoundException" >if resource is not present</exception >
        /// <returns>stream of the resource</returns>
        public Stream Stream()
        {
            var asm = this._container.Value();
            var fullName = this._resourceName.Value();
            var s = asm.GetManifestResourceStream(fullName);

            if (s == null)
            {
                throw new ResourceNotFoundException(fullName, asm);
            }

            return s;
        }
    }
}
