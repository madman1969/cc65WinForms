namespace cc65Wrapper.Enumerations
{
    /// <summary>
    /// Supported CC65 target platforms for Commodore machines.
    /// </summary>
    /// <remarks>
    /// Each enum member corresponds to a cc65/ca65 target name and is intended
    /// to be used when selecting the target platform for compilation and linking.
    /// Use the enum value's name (e.g. <c>CC65ProjectTypes.c64</c>) or call
    /// <c>ToString().ToLowerInvariant()</c> to obtain the usual cc65 target string
    /// (for example, "c64") when passing arguments to the toolchain.
    /// </remarks>
    public enum CC65ProjectTypes
    {
        /// <summary>
        /// Commodore 128.
        /// </summary>
        /// <remarks>
        /// cc65 target name: "c128". 128KB RAM device with native 8502 CPU;
        /// useful for projects targeting C128-specific hardware or modes.
        /// </remarks>
        c128 = 0,

        /// <summary>
        /// Commodore 16 / Plus-compatible 16KB machine.
        /// </summary>
        /// <remarks>
        /// cc65 target name: "c16". Use for C16-family hardware with corresponding
        /// memory and I/O characteristics.
        /// </remarks>
        c16,

        /// <summary>
        /// Commodore 64.
        /// </summary>
        /// <remarks>
        /// cc65 target name: "c64". The most common target in the Commodore 8-bit
        /// family (64KB RAM). Use for C64-specific builds and assets.
        /// </remarks>
        c64,

        /// <summary>
        /// Commodore PET.
        /// </summary>
        /// <remarks>
        /// cc65 target name: "pet". For PET/CBM-series machines with their own
        /// memory map and I/O conventions.
        /// </remarks>
        pet,

        /// <summary>
        /// Commodore Plus/4.
        /// </summary>
        /// <remarks>
        /// cc65 target name: "plus4". Targets the Plus/4 machine (built-in TED chip),
        /// with differing graphics/sound compared to the C64.
        /// </remarks>
        plus4,

        /// <summary>
        /// VIC-20.
        /// </summary>
        /// <remarks>
        /// cc65 target name: "vic20". Low-memory target (typically 5KB–32KB) for
        /// the VIC-20 platform.
        /// </remarks>
        vic20
    }
}
