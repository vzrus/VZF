namespace YAF.Types.Flags
{
  using System;

    /// <summary>
    /// The poll question flags.
    /// </summary>
    [Serializable]
    public class PollFlags : FlagsBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PollFlags"/> class.
        /// </summary>
        public PollFlags()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PollFlags"/> class.
        /// </summary>
        /// <param name="flags">
        /// The flags.
        /// </param>
        public PollFlags(Flags flags)
            : this((int)flags)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PollFlags"/> class.
        /// </summary>
        /// <param name="bitValue">
        /// The bit value.
        /// </param>
        public PollFlags(object bitValue)
            : this((int)bitValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PollFlags"/> class.
        /// </summary>
        /// <param name="bitValue">
        /// The bit value.
        /// </param>
        public PollFlags(int bitValue)
            : base(bitValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PollFlags"/> class.
        /// </summary>
        /// <param name="bits">
        /// The bits.
        /// </param>
        public PollFlags(params bool[] bits)
            : base(bits)
        {
        }

        #endregion

        #region Operators

        /// <summary>
        /// The op_ implicit.
        /// </summary>
        /// <param name="newBitValue">
        /// The new bit value.
        /// </param>
        /// <returns>
        /// </returns>
        public static implicit operator PollFlags(int newBitValue)
        {
            return new PollFlags(newBitValue);
        }

        /// <summary>
        /// The op_ implicit.
        /// </summary>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <returns>
        /// </returns>
        public static implicit operator PollFlags(Flags flags)
        {
            return new PollFlags(flags);
        }

        #endregion

        #region Flags Enumeration

        /// <summary>
        /// Use for bit comparisons
        /// </summary>
        [Flags]
        public enum Flags : int
        {
            /// <summary>
            /// The none.
            /// </summary>
            None = 0,

            /// <summary>
            /// The is closed bound.
            /// </summary>
            IsClosedBound = 4,

            /// <summary>
            /// The allow multiple choices.
            /// </summary>
            AllowMultipleChoices = 8,

            /// <summary>
            /// The show voters.
            /// </summary>
            ShowVoters =16,

            /// <summary>
            /// The allow skip vote.
            /// </summary>
            AllowSkipVote =32

            /* for future use
            xxxxx = 1,
            xxxxx = 2,	
			xxxxx = 64,
			xxxxx = 128,
			xxxxx = 256,
			xxxxx = 512
			*/
        }

        #endregion

        #region Single Flags (can be 32 of them)

        /// <summary>
        /// Gets or sets a value indicating whether is closed bound.
        /// </summary>
        public bool IsClosedBound
        {
            // int value 4
            get
            {
                return this[0];
            }

            set
            {
                this[0] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether allow multiple choices.
        /// </summary>
        public bool AllowMultipleChoices
        {
            // int value 8
            get
            {
                return this[1];
            }

            set
            {
                this[1] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether show voters.
        /// </summary>
        public bool ShowVoters
        {
            // int value 16
            get
            {
                return this[3];
            }

            set
            {
                this[3] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether allow skip vote.
        /// </summary>
        public bool AllowSkipVote
        {
            // int value 32
            get
            {
                return this[3];
            }

            set
            {
                this[3] = value;
            }
        }


        #endregion
    }
}