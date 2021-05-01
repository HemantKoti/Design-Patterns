using System;
using NullObject.Entities;
using NullObject.Services;
using NullObject.View;

namespace NullObject
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args is null)
            
                throw new ArgumentNullException(nameof(args));            

            LearnerService learnerService = new();
            ILearner learner = learnerService.GetCurrentLearner();

            LearnerView view = new(learner);
            view.RenderView();
        }
    }
}