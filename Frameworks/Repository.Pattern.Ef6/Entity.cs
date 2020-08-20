using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;

namespace Repository.Pattern.Ef6
{
    public abstract class Entity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}