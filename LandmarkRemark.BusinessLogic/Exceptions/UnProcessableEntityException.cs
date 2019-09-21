using System;
using System.Collections.Generic;

namespace LandmarkRemark.BusinessLogic.Exceptions
{
    public class UnProcessableEntityException : Exception
    {
        public UnProcessableEntityException()
        {
            Errors=new List<string>();
            ModelStateErrors=new List<ModelStateError>();
        }

        public UnProcessableEntityException(string msg):base(msg)
        {
                
        }
        public IEnumerable<string> Errors { get; set; }

        public IEnumerable<ModelStateError> ModelStateErrors { get; set; }
    }
}