using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

public class AllowCrossSiteJson : ActionFilterAttribute
{
    public virtual void OnActionExecuting(HttpActionContext actionContext)
    {

    }
}