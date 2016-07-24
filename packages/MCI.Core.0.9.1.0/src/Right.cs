﻿//-----------------------------------------------------------------------
// <copyright file="Right.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class Right<L, R> : Either<L, R>
    {
        public Right(R value)
            : base()
        {
            this.Value = value;
        }

        public override bool IsLeft
        {
            get
            {
                return false;
            }
        }

        public override bool IsRight
        {
            get
            {
                return true;
            }
        }


        public readonly R Value;

        public override A Fold<A>(Func<L, A> fl, Func<R, A> fr)
        {
            return fr(this.Value);
        }

        public override Either<R, L> Swap()
        {
            return new Left<R, L>(this.Value);
        }
    }
}