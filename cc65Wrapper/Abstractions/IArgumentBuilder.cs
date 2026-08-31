using System.Collections.Generic;

namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Provides an abstraction for translating an options object into a sequence of
    /// command-line argument tokens for invoking external tools (for example, cc65).
    /// </summary>
    /// <typeparam name="TOptions">
    /// The type that describes the options or settings used to produce the arguments.
    /// This interface is contravariant in <c>TOptions</c> (see the <c>in</c> modifier),
    /// allowing implementations to accept base option types.
    /// </typeparam>
    public interface IArgumentBuilder<in TOptions>
    {
        /// <summary>
        /// Builds the command-line arguments represented as an ordered sequence of tokens.
        /// Implementations should return each argument or flag as a separate string element.
        /// </summary>
        /// <param name="options">The options instance containing values to convert into arguments. May be <c>null</c> if the implementation supports defaults.</param>
        /// <returns>
        /// An <see cref="IEnumerable{String}"/> containing the argument tokens in the order
        /// they should be passed to the executable. The returned sequence must not include
        /// the executable name itself; it should contain only the arguments.
        /// </returns>
        /// <remarks>
        /// - The sequence should preserve ordering for options where order matters.
        /// - Implementations must ensure tokens are already escaped or quoted as required by
        ///   the target process invocation method, or clearly document that callers are
        ///   responsible for quoting/escaping.
        /// - Prefer returning an empty sequence rather than <c>null</c> when there are no arguments.
        /// </remarks>
        /// <example>
        /// Example usage:
        /// <code language="csharp">
        /// // Assume MyOptions -> produces "-o", "out.asm", "-I", "include"
        /// IArgumentBuilder&lt;MyOptions&gt; builder = new MyArgumentBuilder();
        /// IEnumerable&lt;string&gt; args = builder.Build(myOptions);
        /// ProcessStartInfo psi = new ProcessStartInfo("cc65")
        /// {
        ///     ArgumentList = { /* add range of args here */ }
        /// };
        /// </code>
        /// </example>
        IEnumerable<string> Build(TOptions options);
    }
}
