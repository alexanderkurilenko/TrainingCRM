// Место для кода.
function formOnLoad() {
    registerEvents();
    setUpRequirements();
}

function formOnSave(executionObj) {
    reloadRequirements();
    validateCountryCode(executionObj);
    validateMobilePhone(executionObj);
    setUpRequirements();
}

function registerEvents() {
    Xrm.Page.getAttribute("address1_country").addOnChange(reloadRequirements);
    Xrm.Page.getAttribute("address1_city").addOnChange(reloadRequirements);
    Xrm.Page.getAttribute("address1_postalcode").addOnChange(reloadRequirements);
}

function setUpRequirements() {
    if (checkForRequirement()) {
        Xrm.Page.getAttribute("address1_country").setRequiredLevel('required');
    } else {
        Xrm.Page.getAttribute("address1_country").setRequiredLevel('none');
    }
}

function validateCountryCode(executionObj) {
    var countryCodeLength;
    var countryCode = Xrm.Page.getAttribute("address1_country").getValue();
    var pattern = /^[а-яА-Яa-zA-Z]+$/;

    if (countryCode !== null && pattern.test(countryCode)) {
        countryCodeLength = countryCode.length;
    }

    if (countryCode === null) {
        countryCodeLength = 0;
    }

    if (countryCodeLength  !== 2 && countryCodeLength  !==0 ) {
        Xrm.Utility.alertDialog("Enter Country (2 chars required)");
        executionObj.getEventArgs().preventDefault();
    }
    
}

function checkForRequirement() {
    if (Xrm.Page.getAttribute("address1_postalcode").getValue() !== null
        || Xrm.Page.getAttribute("address1_city").getValue() !== null) {
        return true;
    } else {
        return false;
    }
}

function reloadRequirements(){
    if (Xrm.Page.getAttribute("address1_postalcode").getValue() === null
        && Xrm.Page.getAttribute("address1_city").getValue() === null) {
        Xrm.Page.getAttribute("address1_country").setRequiredLevel('none');
    } else {
         Xrm.Page.getAttribute("address1_country").setRequiredLevel('required');
    }
}

function validateMobilePhone(executionObj) {
    var mobilePhone = Xrm.Page.getAttribute("mobilephone").getValue();
    var businessPhone = Xrm.Page.getAttribute("telephone1").getValue();
    var pattern = /^[\+][1-9]\d+$/;

    if (!pattern.test(mobilePhone) && mobilePhone !== null
        || !pattern.test(businessPhone) && businessPhone !== null
    ){
        Xrm.Utility.alertDialog("Please enter Phone Number in valid format e.g. +41 79 333 22 11");
        executionObj.getEventArgs().preventDefault();
    }
}