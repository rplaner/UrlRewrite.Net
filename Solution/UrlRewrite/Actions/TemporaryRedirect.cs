﻿using System.Web;
using System.Xml.Linq;
using UrlRewrite.Interfaces;

namespace UrlRewrite.Actions
{
    internal class TemporaryRedirect : Action, IAction
    {
        public TemporaryRedirect(bool stopProcessing = true, bool endRequest = true)
        {
            _stopProcessing = stopProcessing;
            _endRequest = endRequest;
        }

        public void PerformAction(
            IRequestInfo requestInfo,
            IRuleResult ruleResult,
            out bool stopProcessing,
            out bool endRequest)
        {
            if (requestInfo.ExecutionMode != ExecutionMode.TraceOnly)
            {
                var url = BuildNewUrl(requestInfo);
                requestInfo.DeferredActions.Add(ri => ri.Context.Response.Redirect(url));
            }

            stopProcessing = _stopProcessing;
            endRequest = _endRequest;
        }

        public override string ToString()
        {
            return "Temporarily redirect to new URL";
        }

        public void Initialize(XElement configuration)
        {
        }

        public string ToString(IRequestInfo request)
        {
            var url = BuildNewUrl(request);
            return "temporary redirect to '" + url + "'";
        }
    }
}