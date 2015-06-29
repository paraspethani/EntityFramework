﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Metadata.ModelConventions
{
    public abstract class PropertyAttributeConvention<TAttribute> : IPropertyConvention
        where TAttribute : Attribute
    {
        public virtual InternalPropertyBuilder Apply(InternalPropertyBuilder propertyBuilder)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));

            var clrType = propertyBuilder.Metadata.EntityType.ClrType;
            var propertyName = propertyBuilder.Metadata.Name;

            var attributes = clrType?.GetProperty(propertyName)?.GetCustomAttributes<TAttribute>(true);
            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    Apply(propertyBuilder, attribute);
                }
            }
            return propertyBuilder;
        }

        public abstract void Apply([NotNull] InternalPropertyBuilder propertyBuilder, [NotNull] TAttribute attribute);
    }
}