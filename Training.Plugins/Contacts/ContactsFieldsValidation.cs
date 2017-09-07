using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Training.Plugins.Contacts
{
    public class ContactsFieldsValidation : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService =
                (ITracingService)serviceProvider.GetService(typeof(ITracingService));


            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));


            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {

                Entity entity = (Entity)context.InputParameters["Target"];


                if (entity.LogicalName != "contact")
                    throw new InvalidPluginExecutionException("Plugin's required entity is not correspond");

                try
                {
                    EnableValidation(entity, context);
                }

                catch (Exception ex)
                {
                    tracingService.Trace("MyPlugin: {0}", ex.ToString());
                    throw;
                }
            }
        }

        public void EnableValidation(Entity entity, IPluginExecutionContext context)
        {
            ValidateContactPhone(entity, "mobilephone");
            ValidateContactPhone(entity, "telephone1");
            ValidateContactAddress(context);

        }

        private void ValidateContactPhone(Entity entity, string attribute)
        {
            var value = String.Empty;
            if (entity.Attributes.Contains(attribute))
                value = entity.GetAttributeValue<string>(attribute);

            if (!ValidateMobilePhone(value))
            {
                throw new InvalidPluginExecutionException("Enter valid  " + attribute);
            }

        }

        private static bool ValidateMobilePhone(string phone)
        {
            Regex regex = new Regex(@"^[\+][1-9]\d+$");

            if (!String.IsNullOrEmpty(phone) && !regex.IsMatch(phone))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool ValidateCountryCode(string code)
        {
            Regex regex = new Regex(@"^[а-яА-Яa-zA-Z]{2}$");

            if (!String.IsNullOrEmpty(code) && !regex.IsMatch(code))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks if any of city or zipcode fields are entered,if any then country code becomes required.
        /// </summary>
        /// <param name="entity">The target entity from the input parameters.</param>
        private void ValidateContactAddress(IPluginExecutionContext context)
        {
            Entity preUpdateEntity = context.PostEntityImages["PreUpdateImage"];
            string countryCode = preUpdateEntity.GetAttributeValue<string>("address1_country");
            string postalcode = preUpdateEntity.GetAttributeValue<string>("address1_postalcode");
            string city = preUpdateEntity.GetAttributeValue<string>("address1_city");

            if (!String.IsNullOrEmpty(postalcode) || !String.IsNullOrEmpty(city))
            {

                if (String.IsNullOrWhiteSpace(countryCode) || !ValidateCountryCode(countryCode))
                {
                    throw new InvalidPluginExecutionException("Enter valid Country Code (2 chars required) : ");
                }
            }
            else
            {
                if (!ValidateCountryCode(countryCode))
                    throw new InvalidPluginExecutionException("Enter valid Country Code (2 chars required) : ");
            }
        }
    }
}
