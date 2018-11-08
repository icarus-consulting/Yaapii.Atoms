using System;
using System.IO;
using System.Reflection;
using Yaapii.Atoms.IO.Error;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <para>A embedded resource.</para>
    /// </summary>
    public class ResourceOf : IInput
    {
        private readonly string name;
        private readonly IScalar<Assembly> container;

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
        public ResourceOf(string name, IScalar<Assembly> container)
        {
            this.name = name;
            this.container = container;
        }

        /// <summary>
        /// Stream of the resource.
        /// </summary>
        /// <exception cref = "ResourceNotFoundException" >if resource is not present</exception >
        /// <returns>stream of the resource</returns>
        public Stream Stream()
        {
            var asm = this.container.Value();
            var fullName = FullName();
            var s = asm.GetManifestResourceStream(fullName);

            if (s == null)
            {
                throw new ResourceNotFoundException(fullName, asm);
            }

            return s;
        }

        private string FullName()
        {
            var folders = this.name.Split('\\', '/');
            for (int current = 0; current < folders.Length - 1; current++)
            {
                folders[current] =
                    DashesEncoded(
                        NumbersEncoded(
                            DotsEncoded(folders[current])
                        )
                    );
            }
            return container.Value().GetName().Name + "." + String.Join(".", folders);
        }

        /// <summary>
        /// Replace all occurence of '.' by "._"
        /// Example: "_version8.1" -> "_version8._1"
        /// </summary>
        /// <param name="path">The path to translate</param>
        /// <returns>Result</returns>
        private string DotsEncoded(string path)
        {
            var pieces = path.Split('.');
            for (int idx = 1; idx < pieces.Length; idx++)
            {
                if (pieces[idx].StartsWith("_"))
                {
                    pieces[idx] = pieces[idx].Substring(1);
                }
            }
            path = String.Join("._", pieces);
            return path;
        }

        /// <summary>
        /// When the folder begins with a digit it will be amended by a leading '_'.
        /// Example: "7text7" -> "_7text7"
        /// </summary>
        /// <param name="folder">The folder to translate</param>
        /// <returns>Result</returns>
        private string NumbersEncoded(string folder)
        {
            if (folder.Length > 0)
            {
                var first = folder.Substring(0, 1).ToCharArray()[0];
                if (first >= '0' && first <= '9')
                {
                    folder = "_" + folder;
                }
            }
            return folder;
        }

        /// <summary>
        /// Replaces all '-' by '_'.
        /// When the folder name is equal "-" is will be replaced by "__".
        /// Example: "unique-name" -> "unique_name"
        /// Example: "-" -> "__"
        /// </summary>
        /// <param name="folder">The folder to translate</param>
        /// <returns>Result</returns>
        private string DashesEncoded(string folder)
        {
            if (folder.Equals("-"))
            {
                folder = "__";
            }
            folder = folder.Replace("-", "_");
            return folder;
        }
    }
}
