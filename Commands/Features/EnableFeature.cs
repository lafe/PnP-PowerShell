﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using SharePointPnP.PowerShell.Commands.Base.PipeBinds;
using SharePointPnP.PowerShell.Commands.Enums;

namespace SharePointPnP.PowerShell.Commands.Features
{
    [Cmdlet("Enable", "SPOFeature")]
    [CmdletHelp("Enables a feature", Category = CmdletHelpCategory.Features)]
    [CmdletExample(
        Code = "PS:> Enable-SPOFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe", 
        Remarks = @"This will enable the feature with the id ""99a00f6e-fb81-4dc7-8eac-e09c6f9132fe""", 
        SortOrder = 1)]
    [CmdletExample(
        Code = "PS:> Enable-SPOFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Force", 
        Remarks = @"This will enable the feature with the id ""99a00f6e-fb81-4dc7-8eac-e09c6f9132fe"" with force.", 
        SortOrder = 2)]
    [CmdletExample(
        Code = "PS:> Enable-SPOFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Web",
        Remarks = @"This will enable the feature with the id ""99a00f6e-fb81-4dc7-8eac-e09c6f9132fe"" with the web scope.",  
        SortOrder = 3)]
    public class EnableFeature : SPOWebCmdlet
    {
        [Parameter(Mandatory = true, Position=0, ValueFromPipeline=true, HelpMessage = "The id of the feature to enable.")]
        public GuidPipeBind Identity;

        [Parameter(Mandatory = false, HelpMessage = "Forcibly enable the feature.")]
        public SwitchParameter Force;

        [Parameter(Mandatory = false, HelpMessage = "Specify the scope of the feature to active, either Web or Site. Defaults to Web.")]
        public FeatureScope Scope = FeatureScope.Web;

        [Parameter(Mandatory = false, HelpMessage = "Specify this parameter if the feature you're trying to active is part of a sandboxed solution.")]
        public SwitchParameter Sandboxed;


        protected override void ExecuteCmdlet()
        {
            var featureId = Identity.Id;
            if(Scope == FeatureScope.Web)
            {
                SelectedWeb.ActivateFeature(featureId, Sandboxed);
            }
            else
            {
                ClientContext.Site.ActivateFeature(featureId, Sandboxed);
            }
        }

    }
}
