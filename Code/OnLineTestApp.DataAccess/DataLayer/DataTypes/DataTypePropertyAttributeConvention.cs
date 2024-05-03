﻿using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace OnlineTestApp.DataAccess.DataLayer.DataTypes
{
    internal class DataTypePropertyAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<DataTypeAttribute>
    {
        public override void Apply(ConventionPrimitivePropertyConfiguration configuration,
            DataTypeAttribute attribute)
        {
            if (attribute.DataType == DataType.Date)
            {
                configuration.HasColumnType("Date");
            }
        }
    }
}
