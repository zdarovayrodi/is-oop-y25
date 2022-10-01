// <copyright file="IsuException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Elements should be documented
#pragma warning disable SA1600

namespace Isu.Tools
{
    using System;

    public class IsuException : Exception
    {
        public IsuException(string message)
            : base(message)
        {
        }
    }
}