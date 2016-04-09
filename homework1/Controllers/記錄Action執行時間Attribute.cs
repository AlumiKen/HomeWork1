using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace homework1.Controllers
{
    public class 記錄Action執行時間Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.dtStart = DateTime.Now;
            
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.dtEnd = DateTime.Now;
            var dtSpan = (DateTime)filterContext.Controller.ViewBag.dtEnd
                - (DateTime)filterContext.Controller.ViewBag.dtStart;

            //filterContext.Controller.ViewBag.dtSpan = dtSpan;
            var ActionExcuteLog = "Controller : " + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + ", Action : " + filterContext.ActionDescriptor.ActionName + ", ExcuteTime : " + dtSpan;
            Debug.WriteLine(ActionExcuteLog);

            base.OnActionExecuted(filterContext);
        }
    }
}