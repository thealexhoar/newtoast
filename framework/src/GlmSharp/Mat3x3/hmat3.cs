using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Numerics;
using System.Linq;
using GlmSharp.Swizzle;

// ReSharper disable InconsistentNaming

namespace GlmSharp
{

    /// <summary>
    /// A matrix of type GlmHalf with 3 columns and 3 rows.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "mat")]
    [StructLayout(LayoutKind.Sequential)]
    public struct hmat3 : IReadOnlyList<GlmHalf>, IEquatable<hmat3>
    {

        #region Fields

        /// <summary>
        /// Column 0, Rows 0
        /// </summary>
        [DataMember]
        public GlmHalf m00;

        /// <summary>
        /// Column 0, Rows 1
        /// </summary>
        [DataMember]
        public GlmHalf m01;

        /// <summary>
        /// Column 0, Rows 2
        /// </summary>
        [DataMember]
        public GlmHalf m02;

        /// <summary>
        /// Column 1, Rows 0
        /// </summary>
        [DataMember]
        public GlmHalf m10;

        /// <summary>
        /// Column 1, Rows 1
        /// </summary>
        [DataMember]
        public GlmHalf m11;

        /// <summary>
        /// Column 1, Rows 2
        /// </summary>
        [DataMember]
        public GlmHalf m12;

        /// <summary>
        /// Column 2, Rows 0
        /// </summary>
        [DataMember]
        public GlmHalf m20;

        /// <summary>
        /// Column 2, Rows 1
        /// </summary>
        [DataMember]
        public GlmHalf m21;

        /// <summary>
        /// Column 2, Rows 2
        /// </summary>
        [DataMember]
        public GlmHalf m22;

        #endregion


        #region Constructors

        /// <summary>
        /// Component-wise constructor
        /// </summary>
        public hmat3(GlmHalf m00, GlmHalf m01, GlmHalf m02, GlmHalf m10, GlmHalf m11, GlmHalf m12, GlmHalf m20, GlmHalf m21, GlmHalf m22)
        {
            this.m00 = m00;
            this.m01 = m01;
            this.m02 = m02;
            this.m10 = m10;
            this.m11 = m11;
            this.m12 = m12;
            this.m20 = m20;
            this.m21 = m21;
            this.m22 = m22;
        }

        /// <summary>
        /// Constructs this matrix from a hmat2. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hmat2 m)
        {
            this.m00 = m.m00;
            this.m01 = m.m01;
            this.m02 = GlmHalf.Zero;
            this.m10 = m.m10;
            this.m11 = m.m11;
            this.m12 = GlmHalf.Zero;
            this.m20 = GlmHalf.Zero;
            this.m21 = GlmHalf.Zero;
            this.m22 = GlmHalf.One;
        }

        /// <summary>
        /// Constructs this matrix from a hmat3x2. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hmat3x2 m)
        {
            this.m00 = m.m00;
            this.m01 = m.m01;
            this.m02 = GlmHalf.Zero;
            this.m10 = m.m10;
            this.m11 = m.m11;
            this.m12 = GlmHalf.Zero;
            this.m20 = m.m20;
            this.m21 = m.m21;
            this.m22 = GlmHalf.One;
        }

        /// <summary>
        /// Constructs this matrix from a hmat4x2. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hmat4x2 m)
        {
            this.m00 = m.m00;
            this.m01 = m.m01;
            this.m02 = GlmHalf.Zero;
            this.m10 = m.m10;
            this.m11 = m.m11;
            this.m12 = GlmHalf.Zero;
            this.m20 = m.m20;
            this.m21 = m.m21;
            this.m22 = GlmHalf.One;
        }

        /// <summary>
        /// Constructs this matrix from a hmat2x3. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hmat2x3 m)
        {
            this.m00 = m.m00;
            this.m01 = m.m01;
            this.m02 = m.m02;
            this.m10 = m.m10;
            this.m11 = m.m11;
            this.m12 = m.m12;
            this.m20 = GlmHalf.Zero;
            this.m21 = GlmHalf.Zero;
            this.m22 = GlmHalf.One;
        }

        /// <summary>
        /// Constructs this matrix from a hmat3. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hmat3 m)
        {
            this.m00 = m.m00;
            this.m01 = m.m01;
            this.m02 = m.m02;
            this.m10 = m.m10;
            this.m11 = m.m11;
            this.m12 = m.m12;
            this.m20 = m.m20;
            this.m21 = m.m21;
            this.m22 = m.m22;
        }

        /// <summary>
        /// Constructs this matrix from a hmat4x3. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hmat4x3 m)
        {
            this.m00 = m.m00;
            this.m01 = m.m01;
            this.m02 = m.m02;
            this.m10 = m.m10;
            this.m11 = m.m11;
            this.m12 = m.m12;
            this.m20 = m.m20;
            this.m21 = m.m21;
            this.m22 = m.m22;
        }

        /// <summary>
        /// Constructs this matrix from a hmat2x4. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hmat2x4 m)
        {
            this.m00 = m.m00;
            this.m01 = m.m01;
            this.m02 = m.m02;
            this.m10 = m.m10;
            this.m11 = m.m11;
            this.m12 = m.m12;
            this.m20 = GlmHalf.Zero;
            this.m21 = GlmHalf.Zero;
            this.m22 = GlmHalf.One;
        }

        /// <summary>
        /// Constructs this matrix from a hmat3x4. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hmat3x4 m)
        {
            this.m00 = m.m00;
            this.m01 = m.m01;
            this.m02 = m.m02;
            this.m10 = m.m10;
            this.m11 = m.m11;
            this.m12 = m.m12;
            this.m20 = m.m20;
            this.m21 = m.m21;
            this.m22 = m.m22;
        }

        /// <summary>
        /// Constructs this matrix from a hmat4. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hmat4 m)
        {
            this.m00 = m.m00;
            this.m01 = m.m01;
            this.m02 = m.m02;
            this.m10 = m.m10;
            this.m11 = m.m11;
            this.m12 = m.m12;
            this.m20 = m.m20;
            this.m21 = m.m21;
            this.m22 = m.m22;
        }

        /// <summary>
        /// Constructs this matrix from a series of column vectors. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hvec2 c0, hvec2 c1)
        {
            this.m00 = c0.x;
            this.m01 = c0.y;
            this.m02 = GlmHalf.Zero;
            this.m10 = c1.x;
            this.m11 = c1.y;
            this.m12 = GlmHalf.Zero;
            this.m20 = GlmHalf.Zero;
            this.m21 = GlmHalf.Zero;
            this.m22 = GlmHalf.One;
        }

        /// <summary>
        /// Constructs this matrix from a series of column vectors. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hvec2 c0, hvec2 c1, hvec2 c2)
        {
            this.m00 = c0.x;
            this.m01 = c0.y;
            this.m02 = GlmHalf.Zero;
            this.m10 = c1.x;
            this.m11 = c1.y;
            this.m12 = GlmHalf.Zero;
            this.m20 = c2.x;
            this.m21 = c2.y;
            this.m22 = GlmHalf.One;
        }

        /// <summary>
        /// Constructs this matrix from a series of column vectors. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hvec3 c0, hvec3 c1)
        {
            this.m00 = c0.x;
            this.m01 = c0.y;
            this.m02 = c0.z;
            this.m10 = c1.x;
            this.m11 = c1.y;
            this.m12 = c1.z;
            this.m20 = GlmHalf.Zero;
            this.m21 = GlmHalf.Zero;
            this.m22 = GlmHalf.One;
        }

        /// <summary>
        /// Constructs this matrix from a series of column vectors. Non-overwritten fields are from an Identity matrix.
        /// </summary>
        public hmat3(hvec3 c0, hvec3 c1, hvec3 c2)
        {
            this.m00 = c0.x;
            this.m01 = c0.y;
            this.m02 = c0.z;
            this.m10 = c1.x;
            this.m11 = c1.y;
            this.m12 = c1.z;
            this.m20 = c2.x;
            this.m21 = c2.y;
            this.m22 = c2.z;
        }

        /// <summary>
        /// Creates a rotation matrix from a hquat.
        /// </summary>
        public hmat3(hquat  q)
            : this(q.ToMat3)
        {
        }

        #endregion


        #region Explicit Operators

        /// <summary>
        /// Creates a rotation matrix from a hquat.
        /// </summary>
        public static explicit operator hmat3(hquat  q) => q.ToMat3;

        #endregion


        #region Properties

        /// <summary>
        /// Creates a 2D array with all values (address: Values[x, y])
        /// </summary>
        public GlmHalf[,] Values => new[,] { { m00, m01, m02 }, { m10, m11, m12 }, { m20, m21, m22 } };

        /// <summary>
        /// Creates a 1D array with all values (internal order)
        /// </summary>
        public GlmHalf[] Values1D => new[] { m00, m01, m02, m10, m11, m12, m20, m21, m22 };

        /// <summary>
        /// Gets or sets the column nr 0
        /// </summary>
        public hvec3 Column0
        {
            get
            {
                return new hvec3(m00, m01, m02);
            }
            set
            {
                m00 = value.x;
                m01 = value.y;
                m02 = value.z;
            }
        }

        /// <summary>
        /// Gets or sets the column nr 1
        /// </summary>
        public hvec3 Column1
        {
            get
            {
                return new hvec3(m10, m11, m12);
            }
            set
            {
                m10 = value.x;
                m11 = value.y;
                m12 = value.z;
            }
        }

        /// <summary>
        /// Gets or sets the column nr 2
        /// </summary>
        public hvec3 Column2
        {
            get
            {
                return new hvec3(m20, m21, m22);
            }
            set
            {
                m20 = value.x;
                m21 = value.y;
                m22 = value.z;
            }
        }

        /// <summary>
        /// Gets or sets the row nr 0
        /// </summary>
        public hvec3 Row0
        {
            get
            {
                return new hvec3(m00, m10, m20);
            }
            set
            {
                m00 = value.x;
                m10 = value.y;
                m20 = value.z;
            }
        }

        /// <summary>
        /// Gets or sets the row nr 1
        /// </summary>
        public hvec3 Row1
        {
            get
            {
                return new hvec3(m01, m11, m21);
            }
            set
            {
                m01 = value.x;
                m11 = value.y;
                m21 = value.z;
            }
        }

        /// <summary>
        /// Gets or sets the row nr 2
        /// </summary>
        public hvec3 Row2
        {
            get
            {
                return new hvec3(m02, m12, m22);
            }
            set
            {
                m02 = value.x;
                m12 = value.y;
                m22 = value.z;
            }
        }

        /// <summary>
        /// Creates a quaternion from the rotational part of this matrix.
        /// </summary>
        public hquat ToQuaternion => hquat.FromMat3(this);

        #endregion


        #region Static Properties

        /// <summary>
        /// Predefined all-zero matrix
        /// </summary>
        public static hmat3 Zero { get; } = new hmat3(GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero);

        /// <summary>
        /// Predefined all-ones matrix
        /// </summary>
        public static hmat3 Ones { get; } = new hmat3(GlmHalf.One, GlmHalf.One, GlmHalf.One, GlmHalf.One, GlmHalf.One, GlmHalf.One, GlmHalf.One, GlmHalf.One, GlmHalf.One);

        /// <summary>
        /// Predefined identity matrix
        /// </summary>
        public static hmat3 Identity { get; } = new hmat3(GlmHalf.One, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.One, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.One);

        /// <summary>
        /// Predefined all-MaxValue matrix
        /// </summary>
        public static hmat3 AllMaxValue { get; } = new hmat3(GlmHalf.MaxValue, GlmHalf.MaxValue, GlmHalf.MaxValue, GlmHalf.MaxValue, GlmHalf.MaxValue, GlmHalf.MaxValue, GlmHalf.MaxValue, GlmHalf.MaxValue, GlmHalf.MaxValue);

        /// <summary>
        /// Predefined diagonal-MaxValue matrix
        /// </summary>
        public static hmat3 DiagonalMaxValue { get; } = new hmat3(GlmHalf.MaxValue, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.MaxValue, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.MaxValue);

        /// <summary>
        /// Predefined all-MinValue matrix
        /// </summary>
        public static hmat3 AllMinValue { get; } = new hmat3(GlmHalf.MinValue, GlmHalf.MinValue, GlmHalf.MinValue, GlmHalf.MinValue, GlmHalf.MinValue, GlmHalf.MinValue, GlmHalf.MinValue, GlmHalf.MinValue, GlmHalf.MinValue);

        /// <summary>
        /// Predefined diagonal-MinValue matrix
        /// </summary>
        public static hmat3 DiagonalMinValue { get; } = new hmat3(GlmHalf.MinValue, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.MinValue, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.MinValue);

        /// <summary>
        /// Predefined all-Epsilon matrix
        /// </summary>
        public static hmat3 AllEpsilon { get; } = new hmat3(GlmHalf.Epsilon, GlmHalf.Epsilon, GlmHalf.Epsilon, GlmHalf.Epsilon, GlmHalf.Epsilon, GlmHalf.Epsilon, GlmHalf.Epsilon, GlmHalf.Epsilon, GlmHalf.Epsilon);

        /// <summary>
        /// Predefined diagonal-Epsilon matrix
        /// </summary>
        public static hmat3 DiagonalEpsilon { get; } = new hmat3(GlmHalf.Epsilon, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Epsilon, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Epsilon);

        /// <summary>
        /// Predefined all-NaN matrix
        /// </summary>
        public static hmat3 AllNaN { get; } = new hmat3(GlmHalf.NaN, GlmHalf.NaN, GlmHalf.NaN, GlmHalf.NaN, GlmHalf.NaN, GlmHalf.NaN, GlmHalf.NaN, GlmHalf.NaN, GlmHalf.NaN);

        /// <summary>
        /// Predefined diagonal-NaN matrix
        /// </summary>
        public static hmat3 DiagonalNaN { get; } = new hmat3(GlmHalf.NaN, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.NaN, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.NaN);

        /// <summary>
        /// Predefined all-NegativeInfinity matrix
        /// </summary>
        public static hmat3 AllNegativeInfinity { get; } = new hmat3(GlmHalf.NegativeInfinity, GlmHalf.NegativeInfinity, GlmHalf.NegativeInfinity, GlmHalf.NegativeInfinity, GlmHalf.NegativeInfinity, GlmHalf.NegativeInfinity, GlmHalf.NegativeInfinity, GlmHalf.NegativeInfinity, GlmHalf.NegativeInfinity);

        /// <summary>
        /// Predefined diagonal-NegativeInfinity matrix
        /// </summary>
        public static hmat3 DiagonalNegativeInfinity { get; } = new hmat3(GlmHalf.NegativeInfinity, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.NegativeInfinity, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.NegativeInfinity);

        /// <summary>
        /// Predefined all-PositiveInfinity matrix
        /// </summary>
        public static hmat3 AllPositiveInfinity { get; } = new hmat3(GlmHalf.PositiveInfinity, GlmHalf.PositiveInfinity, GlmHalf.PositiveInfinity, GlmHalf.PositiveInfinity, GlmHalf.PositiveInfinity, GlmHalf.PositiveInfinity, GlmHalf.PositiveInfinity, GlmHalf.PositiveInfinity, GlmHalf.PositiveInfinity);

        /// <summary>
        /// Predefined diagonal-PositiveInfinity matrix
        /// </summary>
        public static hmat3 DiagonalPositiveInfinity { get; } = new hmat3(GlmHalf.PositiveInfinity, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.PositiveInfinity, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.Zero, GlmHalf.PositiveInfinity);

        #endregion


        #region Functions

        /// <summary>
        /// Returns an enumerator that iterates through all fields.
        /// </summary>
        public IEnumerator<GlmHalf> GetEnumerator()
        {
            yield return m00;
            yield return m01;
            yield return m02;
            yield return m10;
            yield return m11;
            yield return m12;
            yield return m20;
            yield return m21;
            yield return m22;
        }

        /// <summary>
        /// Returns an enumerator that iterates through all fields.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion


        /// <summary>
        /// Returns the number of Fields (3 x 3 = 9).
        /// </summary>
        public int Count => 9;

        /// <summary>
        /// Gets/Sets a specific indexed component (a bit slower than direct access).
        /// </summary>
        public GlmHalf this[int fieldIndex]
        {
            get
            {
                switch (fieldIndex)
                {
                    case 0: return m00;
                    case 1: return m01;
                    case 2: return m02;
                    case 3: return m10;
                    case 4: return m11;
                    case 5: return m12;
                    case 6: return m20;
                    case 7: return m21;
                    case 8: return m22;
                    default: throw new ArgumentOutOfRangeException("fieldIndex");
                }
            }
            set
            {
                switch (fieldIndex)
                {
                    case 0: this.m00 = value; break;
                    case 1: this.m01 = value; break;
                    case 2: this.m02 = value; break;
                    case 3: this.m10 = value; break;
                    case 4: this.m11 = value; break;
                    case 5: this.m12 = value; break;
                    case 6: this.m20 = value; break;
                    case 7: this.m21 = value; break;
                    case 8: this.m22 = value; break;
                    default: throw new ArgumentOutOfRangeException("fieldIndex");
                }
            }
        }

        /// <summary>
        /// Gets/Sets a specific 2D-indexed component (a bit slower than direct access).
        /// </summary>
        public GlmHalf this[int col, int row]
        {
            get
            {
                return this[col * 3 + row];
            }
            set
            {
                this[col * 3 + row] = value;
            }
        }

        /// <summary>
        /// Returns true iff this equals rhs component-wise.
        /// </summary>
        public bool Equals(hmat3 rhs) => ((((m00.Equals(rhs.m00) && m01.Equals(rhs.m01)) && m02.Equals(rhs.m02)) && (m10.Equals(rhs.m10) && m11.Equals(rhs.m11))) && ((m12.Equals(rhs.m12) && m20.Equals(rhs.m20)) && (m21.Equals(rhs.m21) && m22.Equals(rhs.m22))));

        /// <summary>
        /// Returns true iff this equals rhs type- and component-wise.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is hmat3 && Equals((hmat3) obj);
        }

        /// <summary>
        /// Returns true iff this equals rhs component-wise.
        /// </summary>
        public static bool operator ==(hmat3 lhs, hmat3 rhs) => lhs.Equals(rhs);

        /// <summary>
        /// Returns true iff this does not equal rhs (component-wise).
        /// </summary>
        public static bool operator !=(hmat3 lhs, hmat3 rhs) => !lhs.Equals(rhs);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((((((((((((((((m00.GetHashCode()) * 397) ^ m01.GetHashCode()) * 397) ^ m02.GetHashCode()) * 397) ^ m10.GetHashCode()) * 397) ^ m11.GetHashCode()) * 397) ^ m12.GetHashCode()) * 397) ^ m20.GetHashCode()) * 397) ^ m21.GetHashCode()) * 397) ^ m22.GetHashCode();
            }
        }

        /// <summary>
        /// Returns a transposed version of this matrix.
        /// </summary>
        public hmat3 Transposed => new hmat3(m00, m10, m20, m01, m11, m21, m02, m12, m22);

        /// <summary>
        /// Returns the minimal component of this matrix.
        /// </summary>
        public GlmHalf MinElement => GlmHalf.Min(GlmHalf.Min(GlmHalf.Min(GlmHalf.Min(GlmHalf.Min(GlmHalf.Min(GlmHalf.Min(GlmHalf.Min(m00, m01), m02), m10), m11), m12), m20), m21), m22);

        /// <summary>
        /// Returns the maximal component of this matrix.
        /// </summary>
        public GlmHalf MaxElement => GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(m00, m01), m02), m10), m11), m12), m20), m21), m22);

        /// <summary>
        /// Returns the euclidean length of this matrix.
        /// </summary>
        public float Length => (float)Math.Sqrt(((((m00*m00 + m01*m01) + m02*m02) + (m10*m10 + m11*m11)) + ((m12*m12 + m20*m20) + (m21*m21 + m22*m22))));

        /// <summary>
        /// Returns the squared euclidean length of this matrix.
        /// </summary>
        public float LengthSqr => ((((m00*m00 + m01*m01) + m02*m02) + (m10*m10 + m11*m11)) + ((m12*m12 + m20*m20) + (m21*m21 + m22*m22)));

        /// <summary>
        /// Returns the sum of all fields.
        /// </summary>
        public GlmHalf Sum => ((((m00 + m01) + m02) + (m10 + m11)) + ((m12 + m20) + (m21 + m22)));

        /// <summary>
        /// Returns the euclidean norm of this matrix.
        /// </summary>
        public float Norm => (float)Math.Sqrt(((((m00*m00 + m01*m01) + m02*m02) + (m10*m10 + m11*m11)) + ((m12*m12 + m20*m20) + (m21*m21 + m22*m22))));

        /// <summary>
        /// Returns the one-norm of this matrix.
        /// </summary>
        public float Norm1 => ((((GlmHalf.Abs(m00) + GlmHalf.Abs(m01)) + GlmHalf.Abs(m02)) + (GlmHalf.Abs(m10) + GlmHalf.Abs(m11))) + ((GlmHalf.Abs(m12) + GlmHalf.Abs(m20)) + (GlmHalf.Abs(m21) + GlmHalf.Abs(m22))));

        /// <summary>
        /// Returns the two-norm of this matrix.
        /// </summary>
        public float Norm2 => (float)Math.Sqrt(((((m00*m00 + m01*m01) + m02*m02) + (m10*m10 + m11*m11)) + ((m12*m12 + m20*m20) + (m21*m21 + m22*m22))));

        /// <summary>
        /// Returns the max-norm of this matrix.
        /// </summary>
        public GlmHalf NormMax => GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(GlmHalf.Max(GlmHalf.Abs(m00), GlmHalf.Abs(m01)), GlmHalf.Abs(m02)), GlmHalf.Abs(m10)), GlmHalf.Abs(m11)), GlmHalf.Abs(m12)), GlmHalf.Abs(m20)), GlmHalf.Abs(m21)), GlmHalf.Abs(m22));

        /// <summary>
        /// Returns the p-norm of this matrix.
        /// </summary>
        public double NormP(double p) => Math.Pow(((((Math.Pow((double)GlmHalf.Abs(m00), p) + Math.Pow((double)GlmHalf.Abs(m01), p)) + Math.Pow((double)GlmHalf.Abs(m02), p)) + (Math.Pow((double)GlmHalf.Abs(m10), p) + Math.Pow((double)GlmHalf.Abs(m11), p))) + ((Math.Pow((double)GlmHalf.Abs(m12), p) + Math.Pow((double)GlmHalf.Abs(m20), p)) + (Math.Pow((double)GlmHalf.Abs(m21), p) + Math.Pow((double)GlmHalf.Abs(m22), p)))), 1 / p);

        /// <summary>
        /// Returns determinant of this matrix.
        /// </summary>
        public GlmHalf Determinant => m00 * (m11 * m22 - m21 * m12) - m10 * (m01 * m22 - m21 * m02) + m20 * (m01 * m12 - m11 * m02);

        /// <summary>
        /// Returns the adjunct of this matrix.
        /// </summary>
        public hmat3 Adjugate => new hmat3(m11 * m22 - m21 * m12, -m01 * m22 + m21 * m02, m01 * m12 - m11 * m02, -m10 * m22 + m20 * m12, m00 * m22 - m20 * m02, -m00 * m12 + m10 * m02, m10 * m21 - m20 * m11, -m00 * m21 + m20 * m01, m00 * m11 - m10 * m01);

        /// <summary>
        /// Returns the inverse of this matrix (use with caution).
        /// </summary>
        public hmat3 Inverse => Adjugate / Determinant;

        /// <summary>
        /// Executes a matrix-matrix-multiplication hmat3 * hmat2x3 -> hmat2x3.
        /// </summary>
        public static hmat2x3 operator*(hmat3 lhs, hmat2x3 rhs) => new hmat2x3(((lhs.m00 * rhs.m00 + lhs.m10 * rhs.m01) + lhs.m20 * rhs.m02), ((lhs.m01 * rhs.m00 + lhs.m11 * rhs.m01) + lhs.m21 * rhs.m02), ((lhs.m02 * rhs.m00 + lhs.m12 * rhs.m01) + lhs.m22 * rhs.m02), ((lhs.m00 * rhs.m10 + lhs.m10 * rhs.m11) + lhs.m20 * rhs.m12), ((lhs.m01 * rhs.m10 + lhs.m11 * rhs.m11) + lhs.m21 * rhs.m12), ((lhs.m02 * rhs.m10 + lhs.m12 * rhs.m11) + lhs.m22 * rhs.m12));

        /// <summary>
        /// Executes a matrix-matrix-multiplication hmat3 * hmat3 -> hmat3.
        /// </summary>
        public static hmat3 operator*(hmat3 lhs, hmat3 rhs) => new hmat3(((lhs.m00 * rhs.m00 + lhs.m10 * rhs.m01) + lhs.m20 * rhs.m02), ((lhs.m01 * rhs.m00 + lhs.m11 * rhs.m01) + lhs.m21 * rhs.m02), ((lhs.m02 * rhs.m00 + lhs.m12 * rhs.m01) + lhs.m22 * rhs.m02), ((lhs.m00 * rhs.m10 + lhs.m10 * rhs.m11) + lhs.m20 * rhs.m12), ((lhs.m01 * rhs.m10 + lhs.m11 * rhs.m11) + lhs.m21 * rhs.m12), ((lhs.m02 * rhs.m10 + lhs.m12 * rhs.m11) + lhs.m22 * rhs.m12), ((lhs.m00 * rhs.m20 + lhs.m10 * rhs.m21) + lhs.m20 * rhs.m22), ((lhs.m01 * rhs.m20 + lhs.m11 * rhs.m21) + lhs.m21 * rhs.m22), ((lhs.m02 * rhs.m20 + lhs.m12 * rhs.m21) + lhs.m22 * rhs.m22));

        /// <summary>
        /// Executes a matrix-matrix-multiplication hmat3 * hmat4x3 -> hmat4x3.
        /// </summary>
        public static hmat4x3 operator*(hmat3 lhs, hmat4x3 rhs) => new hmat4x3(((lhs.m00 * rhs.m00 + lhs.m10 * rhs.m01) + lhs.m20 * rhs.m02), ((lhs.m01 * rhs.m00 + lhs.m11 * rhs.m01) + lhs.m21 * rhs.m02), ((lhs.m02 * rhs.m00 + lhs.m12 * rhs.m01) + lhs.m22 * rhs.m02), ((lhs.m00 * rhs.m10 + lhs.m10 * rhs.m11) + lhs.m20 * rhs.m12), ((lhs.m01 * rhs.m10 + lhs.m11 * rhs.m11) + lhs.m21 * rhs.m12), ((lhs.m02 * rhs.m10 + lhs.m12 * rhs.m11) + lhs.m22 * rhs.m12), ((lhs.m00 * rhs.m20 + lhs.m10 * rhs.m21) + lhs.m20 * rhs.m22), ((lhs.m01 * rhs.m20 + lhs.m11 * rhs.m21) + lhs.m21 * rhs.m22), ((lhs.m02 * rhs.m20 + lhs.m12 * rhs.m21) + lhs.m22 * rhs.m22), ((lhs.m00 * rhs.m30 + lhs.m10 * rhs.m31) + lhs.m20 * rhs.m32), ((lhs.m01 * rhs.m30 + lhs.m11 * rhs.m31) + lhs.m21 * rhs.m32), ((lhs.m02 * rhs.m30 + lhs.m12 * rhs.m31) + lhs.m22 * rhs.m32));

        /// <summary>
        /// Executes a matrix-vector-multiplication.
        /// </summary>
        public static hvec3 operator*(hmat3 m, hvec3 v) => new hvec3(((m.m00 * v.x + m.m10 * v.y) + m.m20 * v.z), ((m.m01 * v.x + m.m11 * v.y) + m.m21 * v.z), ((m.m02 * v.x + m.m12 * v.y) + m.m22 * v.z));

        /// <summary>
        /// Executes a matrix-matrix-divison A / B == A * B^-1 (use with caution).
        /// </summary>
        public static hmat3 operator/(hmat3 A, hmat3 B) => A * B.Inverse;

        /// <summary>
        /// Executes a component-wise * (multiply).
        /// </summary>
        public static hmat3 CompMul(hmat3 A, hmat3 B) => new hmat3(A.m00 * B.m00, A.m01 * B.m01, A.m02 * B.m02, A.m10 * B.m10, A.m11 * B.m11, A.m12 * B.m12, A.m20 * B.m20, A.m21 * B.m21, A.m22 * B.m22);

        /// <summary>
        /// Executes a component-wise / (divide).
        /// </summary>
        public static hmat3 CompDiv(hmat3 A, hmat3 B) => new hmat3(A.m00 / B.m00, A.m01 / B.m01, A.m02 / B.m02, A.m10 / B.m10, A.m11 / B.m11, A.m12 / B.m12, A.m20 / B.m20, A.m21 / B.m21, A.m22 / B.m22);

        /// <summary>
        /// Executes a component-wise + (add).
        /// </summary>
        public static hmat3 CompAdd(hmat3 A, hmat3 B) => new hmat3(A.m00 + B.m00, A.m01 + B.m01, A.m02 + B.m02, A.m10 + B.m10, A.m11 + B.m11, A.m12 + B.m12, A.m20 + B.m20, A.m21 + B.m21, A.m22 + B.m22);

        /// <summary>
        /// Executes a component-wise - (subtract).
        /// </summary>
        public static hmat3 CompSub(hmat3 A, hmat3 B) => new hmat3(A.m00 - B.m00, A.m01 - B.m01, A.m02 - B.m02, A.m10 - B.m10, A.m11 - B.m11, A.m12 - B.m12, A.m20 - B.m20, A.m21 - B.m21, A.m22 - B.m22);

        /// <summary>
        /// Executes a component-wise + (add).
        /// </summary>
        public static hmat3 operator+(hmat3 lhs, hmat3 rhs) => new hmat3(lhs.m00 + rhs.m00, lhs.m01 + rhs.m01, lhs.m02 + rhs.m02, lhs.m10 + rhs.m10, lhs.m11 + rhs.m11, lhs.m12 + rhs.m12, lhs.m20 + rhs.m20, lhs.m21 + rhs.m21, lhs.m22 + rhs.m22);

        /// <summary>
        /// Executes a component-wise + (add) with a scalar.
        /// </summary>
        public static hmat3 operator+(hmat3 lhs, GlmHalf rhs) => new hmat3(lhs.m00 + rhs, lhs.m01 + rhs, lhs.m02 + rhs, lhs.m10 + rhs, lhs.m11 + rhs, lhs.m12 + rhs, lhs.m20 + rhs, lhs.m21 + rhs, lhs.m22 + rhs);

        /// <summary>
        /// Executes a component-wise + (add) with a scalar.
        /// </summary>
        public static hmat3 operator+(GlmHalf lhs, hmat3 rhs) => new hmat3(lhs + rhs.m00, lhs + rhs.m01, lhs + rhs.m02, lhs + rhs.m10, lhs + rhs.m11, lhs + rhs.m12, lhs + rhs.m20, lhs + rhs.m21, lhs + rhs.m22);

        /// <summary>
        /// Executes a component-wise - (subtract).
        /// </summary>
        public static hmat3 operator-(hmat3 lhs, hmat3 rhs) => new hmat3(lhs.m00 - rhs.m00, lhs.m01 - rhs.m01, lhs.m02 - rhs.m02, lhs.m10 - rhs.m10, lhs.m11 - rhs.m11, lhs.m12 - rhs.m12, lhs.m20 - rhs.m20, lhs.m21 - rhs.m21, lhs.m22 - rhs.m22);

        /// <summary>
        /// Executes a component-wise - (subtract) with a scalar.
        /// </summary>
        public static hmat3 operator-(hmat3 lhs, GlmHalf rhs) => new hmat3(lhs.m00 - rhs, lhs.m01 - rhs, lhs.m02 - rhs, lhs.m10 - rhs, lhs.m11 - rhs, lhs.m12 - rhs, lhs.m20 - rhs, lhs.m21 - rhs, lhs.m22 - rhs);

        /// <summary>
        /// Executes a component-wise - (subtract) with a scalar.
        /// </summary>
        public static hmat3 operator-(GlmHalf lhs, hmat3 rhs) => new hmat3(lhs - rhs.m00, lhs - rhs.m01, lhs - rhs.m02, lhs - rhs.m10, lhs - rhs.m11, lhs - rhs.m12, lhs - rhs.m20, lhs - rhs.m21, lhs - rhs.m22);

        /// <summary>
        /// Executes a component-wise / (divide) with a scalar.
        /// </summary>
        public static hmat3 operator/(hmat3 lhs, GlmHalf rhs) => new hmat3(lhs.m00 / rhs, lhs.m01 / rhs, lhs.m02 / rhs, lhs.m10 / rhs, lhs.m11 / rhs, lhs.m12 / rhs, lhs.m20 / rhs, lhs.m21 / rhs, lhs.m22 / rhs);

        /// <summary>
        /// Executes a component-wise / (divide) with a scalar.
        /// </summary>
        public static hmat3 operator/(GlmHalf lhs, hmat3 rhs) => new hmat3(lhs / rhs.m00, lhs / rhs.m01, lhs / rhs.m02, lhs / rhs.m10, lhs / rhs.m11, lhs / rhs.m12, lhs / rhs.m20, lhs / rhs.m21, lhs / rhs.m22);

        /// <summary>
        /// Executes a component-wise * (multiply) with a scalar.
        /// </summary>
        public static hmat3 operator*(hmat3 lhs, GlmHalf rhs) => new hmat3(lhs.m00 * rhs, lhs.m01 * rhs, lhs.m02 * rhs, lhs.m10 * rhs, lhs.m11 * rhs, lhs.m12 * rhs, lhs.m20 * rhs, lhs.m21 * rhs, lhs.m22 * rhs);

        /// <summary>
        /// Executes a component-wise * (multiply) with a scalar.
        /// </summary>
        public static hmat3 operator*(GlmHalf lhs, hmat3 rhs) => new hmat3(lhs * rhs.m00, lhs * rhs.m01, lhs * rhs.m02, lhs * rhs.m10, lhs * rhs.m11, lhs * rhs.m12, lhs * rhs.m20, lhs * rhs.m21, lhs * rhs.m22);

        /// <summary>
        /// Executes a component-wise lesser-than comparison.
        /// </summary>
        public static bmat3 operator<(hmat3 lhs, hmat3 rhs) => new bmat3(lhs.m00 < rhs.m00, lhs.m01 < rhs.m01, lhs.m02 < rhs.m02, lhs.m10 < rhs.m10, lhs.m11 < rhs.m11, lhs.m12 < rhs.m12, lhs.m20 < rhs.m20, lhs.m21 < rhs.m21, lhs.m22 < rhs.m22);

        /// <summary>
        /// Executes a component-wise lesser-than comparison with a scalar.
        /// </summary>
        public static bmat3 operator<(hmat3 lhs, GlmHalf rhs) => new bmat3(lhs.m00 < rhs, lhs.m01 < rhs, lhs.m02 < rhs, lhs.m10 < rhs, lhs.m11 < rhs, lhs.m12 < rhs, lhs.m20 < rhs, lhs.m21 < rhs, lhs.m22 < rhs);

        /// <summary>
        /// Executes a component-wise lesser-than comparison with a scalar.
        /// </summary>
        public static bmat3 operator<(GlmHalf lhs, hmat3 rhs) => new bmat3(lhs < rhs.m00, lhs < rhs.m01, lhs < rhs.m02, lhs < rhs.m10, lhs < rhs.m11, lhs < rhs.m12, lhs < rhs.m20, lhs < rhs.m21, lhs < rhs.m22);

        /// <summary>
        /// Executes a component-wise lesser-or-equal comparison.
        /// </summary>
        public static bmat3 operator<=(hmat3 lhs, hmat3 rhs) => new bmat3(lhs.m00 <= rhs.m00, lhs.m01 <= rhs.m01, lhs.m02 <= rhs.m02, lhs.m10 <= rhs.m10, lhs.m11 <= rhs.m11, lhs.m12 <= rhs.m12, lhs.m20 <= rhs.m20, lhs.m21 <= rhs.m21, lhs.m22 <= rhs.m22);

        /// <summary>
        /// Executes a component-wise lesser-or-equal comparison with a scalar.
        /// </summary>
        public static bmat3 operator<=(hmat3 lhs, GlmHalf rhs) => new bmat3(lhs.m00 <= rhs, lhs.m01 <= rhs, lhs.m02 <= rhs, lhs.m10 <= rhs, lhs.m11 <= rhs, lhs.m12 <= rhs, lhs.m20 <= rhs, lhs.m21 <= rhs, lhs.m22 <= rhs);

        /// <summary>
        /// Executes a component-wise lesser-or-equal comparison with a scalar.
        /// </summary>
        public static bmat3 operator<=(GlmHalf lhs, hmat3 rhs) => new bmat3(lhs <= rhs.m00, lhs <= rhs.m01, lhs <= rhs.m02, lhs <= rhs.m10, lhs <= rhs.m11, lhs <= rhs.m12, lhs <= rhs.m20, lhs <= rhs.m21, lhs <= rhs.m22);

        /// <summary>
        /// Executes a component-wise greater-than comparison.
        /// </summary>
        public static bmat3 operator>(hmat3 lhs, hmat3 rhs) => new bmat3(lhs.m00 > rhs.m00, lhs.m01 > rhs.m01, lhs.m02 > rhs.m02, lhs.m10 > rhs.m10, lhs.m11 > rhs.m11, lhs.m12 > rhs.m12, lhs.m20 > rhs.m20, lhs.m21 > rhs.m21, lhs.m22 > rhs.m22);

        /// <summary>
        /// Executes a component-wise greater-than comparison with a scalar.
        /// </summary>
        public static bmat3 operator>(hmat3 lhs, GlmHalf rhs) => new bmat3(lhs.m00 > rhs, lhs.m01 > rhs, lhs.m02 > rhs, lhs.m10 > rhs, lhs.m11 > rhs, lhs.m12 > rhs, lhs.m20 > rhs, lhs.m21 > rhs, lhs.m22 > rhs);

        /// <summary>
        /// Executes a component-wise greater-than comparison with a scalar.
        /// </summary>
        public static bmat3 operator>(GlmHalf lhs, hmat3 rhs) => new bmat3(lhs > rhs.m00, lhs > rhs.m01, lhs > rhs.m02, lhs > rhs.m10, lhs > rhs.m11, lhs > rhs.m12, lhs > rhs.m20, lhs > rhs.m21, lhs > rhs.m22);

        /// <summary>
        /// Executes a component-wise greater-or-equal comparison.
        /// </summary>
        public static bmat3 operator>=(hmat3 lhs, hmat3 rhs) => new bmat3(lhs.m00 >= rhs.m00, lhs.m01 >= rhs.m01, lhs.m02 >= rhs.m02, lhs.m10 >= rhs.m10, lhs.m11 >= rhs.m11, lhs.m12 >= rhs.m12, lhs.m20 >= rhs.m20, lhs.m21 >= rhs.m21, lhs.m22 >= rhs.m22);

        /// <summary>
        /// Executes a component-wise greater-or-equal comparison with a scalar.
        /// </summary>
        public static bmat3 operator>=(hmat3 lhs, GlmHalf rhs) => new bmat3(lhs.m00 >= rhs, lhs.m01 >= rhs, lhs.m02 >= rhs, lhs.m10 >= rhs, lhs.m11 >= rhs, lhs.m12 >= rhs, lhs.m20 >= rhs, lhs.m21 >= rhs, lhs.m22 >= rhs);

        /// <summary>
        /// Executes a component-wise greater-or-equal comparison with a scalar.
        /// </summary>
        public static bmat3 operator>=(GlmHalf lhs, hmat3 rhs) => new bmat3(lhs >= rhs.m00, lhs >= rhs.m01, lhs >= rhs.m02, lhs >= rhs.m10, lhs >= rhs.m11, lhs >= rhs.m12, lhs >= rhs.m20, lhs >= rhs.m21, lhs >= rhs.m22);
    }
}
