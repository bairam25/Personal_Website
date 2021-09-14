// calculate credit price and cash price if cash price margin not setted
function CalCRPrice() {
    try {

        var CRMargin = 0;
        var SupplierPrice = 0;
        if (document.getElementById("txtCRMargin").value != "") {
          
            CRMargin = document.getElementById("txtCRMargin").value;
          
            if (document.getElementById("txtSupplierPrice").value != "") {
                SupplierPrice = document.getElementById("txtSupplierPrice").value.replace(',', '');
            }
            document.getElementById("txtCRPrice").value = (parseFloat(SupplierPrice) * parseFloat(CRMargin) / 100) + parseFloat(SupplierPrice);
            if (document.getElementById("txtCODMargin").value == "") {
                document.getElementById("txtCODMargin").value = CRMargin;
                CalCODPrice();
            }
           
        }

    } catch (err) {
        alert(err);
    }
}
// calculate cash price
function CalCODPrice() {
    try {

        var CODMargin = 0;
        var SupplierPrice = 0;
        if (document.getElementById("txtCODMargin").value != "") {
            CODMargin = document.getElementById("txtCODMargin").value;
            if (document.getElementById("txtSupplierPrice").value != "") {
                SupplierPrice = document.getElementById("txtSupplierPrice").value.replace(',', '');
            }
            document.getElementById("txtCODPrice").value = (parseFloat(SupplierPrice) * parseFloat(CODMargin) / 100) + parseFloat(SupplierPrice);
        }


    } catch (err) {
        alert(err);
    }
}
//calculare vat
function CalVAT(sender) {
    try {
        
        var controlId = sender.id;
        var VATPercent = 0;
        var VAT = 0;
        var CRPRice = 0;
        var CODPrice = 0;
        if (document.getElementById("txtCRPrice").value != "") {
            CRPRice = document.getElementById("txtCRPrice").value.replace(',', '');
        }
        else {
            return;
        }

        if (document.getElementById("txtVATPercent").value != "") {
            VATPercent = document.getElementById("txtVATPercent").value;
            VAT = parseFloat(CRPRice) * parseFloat(VATPercent) / 100;
            document.getElementById("txtVAT").value = VAT;
        }


    } catch (err) {
        alert(err);
    }
}