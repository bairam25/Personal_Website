$(document).ready(function () {

    GetCart('Child');
});
function GetCart(Type) {
    try {
        GetCartSummary(Type);
        if (Type == "Parent") {
            WebService.CartView(onFillCartParent);
        }
        else {
            WebService.CartView(onFillCart);
        }
    }
    catch (err) {
        //alert(err);
    }
}
function onFillCart(res) {
    try {
        var divCart = document.getElementById("items");
        divCart.innerHTML = "";
        if (divCart.innerHTML == "") {
            if (res != "") {
                for (var i = 0; i < res.length; i++) {
                    var AR = res[i].split('<>');
                    var ItemId = AR[0];
                    var SKU = AR[1];
                    var Name = AR[2];
                    var Qty = AR[3];
                    var Photo = AR[4];
                    var Price = AR[5];
                    var ServerURL = AR[6];
                    ///////////// Div Cart Content ////////////////////
                    var divCartContent = document.createElement('div');
                    divCartContent.className = 'cart-content';
                    ///////////// Div container ////////////////////
                    var divcontainer = document.createElement('div');
                    divcontainer.className = 'container-flui';
                    ///////////// Div Row ////////////////////
                    var divRow = document.createElement('div');
                    divRow.className = 'Row';
                    ///////////// Div Column1 ////////////////////
                    var divCol1 = document.createElement('div');
                    divCol1.className = 'col-sm-4 col-md-4 cart-img-wrapper cart-img';
                    ///////////// Item Image ////////////////////
                    var ImgItem = document.createElement('img');
                    ImgItem.id = "imgCartItem";
                    ImgItem.className = 'img-fluid';
                    ImgItem.src = Photo;
                    ImgItem.title = i + 1;
                    divCol1.appendChild(ImgItem);
                    divRow.appendChild(divCol1);

                    ///////////// Div Column2 ////////////////////
                    var divCol2 = document.createElement('div');
                    divCol2.className = 'col-sm-8 col-md-8 dis-inline';
                    ///////////// Div Text ////////////////////
                    var divText = document.createElement('div');
                    divText.className = 'pro-text';
                    /////////// Item Name Anchor //////////////////////
                    var anchorItemName = document.createElement('a');
                    anchorItemName.href = ServerURL + 'Item/' + SKU;
                    anchorItemName.innerHTML = Name;
                    var BR1 = document.createElement('br');
                    //////////////// Qty /////////////////
                    var divQty = document.createElement('span');
                    divQty.innerHTML = "Qty : " + Qty;
                    var BR2 = document.createElement('br');
                    /////////// Delete Item Anchor //////////////////////
                    var anchorDelete = document.createElement('a');
                    anchorDelete.href = '#';
                    anchorDelete.innerHTML = "x";
                    anchorDelete.className = "close";
                    anchorDelete.title = "Remove";
                    anchorDelete.lang = ItemId;
                    anchorDelete.onclick = function () {
                        var r = confirm("Delete item from cart ?");
                        if (r == true) {
                            WebService.DeleteCartItem(this.lang, onDeleteSuccess);
                        } 
                    };
                    //////////////// Qty /////////////////
                    var strongPrice = document.createElement('strong');
                    var divPrice = document.createElement('span');
                    divPrice.innerHTML = Price + ' AED';
                    divText.appendChild(anchorItemName);
                    divText.appendChild(BR1);
                    divText.appendChild(divQty);
                    divText.appendChild(BR2);
                    divText.appendChild(anchorDelete);
                    strongPrice.appendChild(divPrice);
                    divText.appendChild(strongPrice);
                    divCol2.appendChild(divText);
                    divRow.appendChild(divCol2);
                    divcontainer.appendChild(divRow);
                    divCartContent.appendChild(divcontainer);
                    divCart.appendChild(divCartContent);
                    
                }
            }

        }

    }
    catch (err) {
        //alert(err);
    }
}
function onDeleteSuccess(){
    GetCart('Child');
    return false;
}
function onFillCartParent(res) {
    try {
        var divCart = parent.document.getElementById("items");
        divCart.innerHTML = "";
        if (divCart.innerHTML == "") {
            if (res != "") {
                for (var i = 0; i < res.length; i++) {
                    var AR = res[i].split('<>');
                    var ItemId = AR[0];
                    var SKU = AR[1];
                    var Name = AR[2];
                    var Qty = AR[3];
                    var Photo = AR[4];
                    var Price = AR[5];
                    var ServerURL = AR[6];
                    ///////////// Div Cart Content ////////////////////
                    var divCartContent = parent.document.createElement('div');
                    divCartContent.className = 'cart-content';
                    ///////////// Div container ////////////////////
                    var divcontainer = parent.document.createElement('div');
                    divcontainer.className = 'container-flui';
                    ///////////// Div Row ////////////////////
                    var divRow = parent.document.createElement('div');
                    divRow.className = 'Row';
                    ///////////// Div Column1 ////////////////////
                    var divCol1 = parent.document.createElement('div');
                    divCol1.className = 'col-sm-4 col-md-4 cart-img-wrapper cart-img';
                    ///////////// Item Image ////////////////////
                    var ImgItem = parent.document.createElement('img');
                    ImgItem.id = "imgCartItem";
                    ImgItem.className = 'img-fluid';
                    ImgItem.src = Photo;
                    ImgItem.title = i + 1;
                    divCol1.appendChild(ImgItem);
                    divRow.appendChild(divCol1);

                    ///////////// Div Column2 ////////////////////
                    var divCol2 = parent.document.createElement('div');
                    divCol2.className = 'col-sm-8 col-md-8 dis-inline';
                    ///////////// Div Text ////////////////////
                    var divText = parent.document.createElement('div');
                    divText.className = 'pro-text';
                    /////////// Item Name Anchor //////////////////////
                    var anchorItemName = parent.document.createElement('a');
                    anchorItemName.href = ServerURL + 'Item/' + SKU;
                    anchorItemName.innerHTML = Name;
                    var BR1 = parent.document.createElement('br');
                    //////////////// Qty /////////////////
                    var divQty = parent.document.createElement('span');
                    divQty.innerHTML = "Qty : " + Qty;
                    var BR2 = parent.document.createElement('br');
                    /////////// Delete Item Anchor //////////////////////
                    var anchorDelete = parent.document.createElement('a');
                    anchorDelete.href = '#';
                    anchorDelete.innerHTML = "x";
                    anchorDelete.className = "close";
                    anchorDelete.title = "Remove";
                    anchorDelete.lang = ItemId;
                    anchorDelete.onclick = function () {
                        var r = confirm("Delete item from cart ?");
                        if (r == true) {
                            WebService.DeleteCartItem(this.lang, onDeleteSuccess);
                        }
                    };
                    //////////////// Qty /////////////////
                    var strongPrice = parent.document.createElement('strong');
                    var divPrice = parent.document.createElement('span');
                    divPrice.innerHTML = Price + ' AED';
                    divText.appendChild(anchorItemName);
                    divText.appendChild(BR1);
                    divText.appendChild(divQty);
                    divText.appendChild(BR2);
                    divText.appendChild(anchorDelete);
                    strongPrice.appendChild(divPrice);
                    divText.appendChild(strongPrice);
                    divCol2.appendChild(divText);
                    divRow.appendChild(divCol2);
                    divcontainer.appendChild(divRow);
                    divCartContent.appendChild(divcontainer);
                    divCart.appendChild(divCartContent);

                }
            }

        }

    }
    catch (err) {
        //alert(err);
    }
}

function onParentDeleteSuccess() {
    GetCart('Parent');
    return false;
}
function GetCartSummary(Type) {
    try {
        if (Type == "Parent") {
            WebService.CartSummary(onGetSummaryParent);
        }
        else {
            WebService.CartSummary(onGetSummary);
        }

    }
    catch (err) {
        //alert(err);
    }
}
function onGetSummary(res) {
    try {
        var AR = res.split('<>');
        var TotalQty = AR[0];
        var TotalShipping = AR[1];
        var TotalAmount = AR[2];
        document.getElementById("lblNoOfItems").innerHTML = TotalQty;
        document.getElementById("lblShipping").innerHTML = TotalShipping + ' AED';
        document.getElementById("lblTotal").innerHTML = TotalAmount + ' AED';

        if (parseInt(TotalQty) == 0) {
            document.getElementById("cartList").style.display = "none";

        }
        else {
            document.getElementById("cartList").style.display = "";
        }
    }
    catch (err) {
        //alert(err);
    }
}
function onGetSummaryParent(res) {
    try {
        var AR = res.split('<>');
        var TotalQty = AR[0];
        var TotalShipping = AR[1];
        var TotalAmount = AR[2];
        parent.document.getElementById("lblNoOfItems").innerHTML = TotalQty;
        parent.document.getElementById("lblShipping").innerHTML = TotalShipping + ' AED';
        parent.document.getElementById("lblTotal").innerHTML = TotalAmount + ' AED';

        if (parseInt(TotalQty) == 0) {
            parent.document.getElementById("cartList").style.display = "none";

        }
        else {
            parent.document.getElementById("cartList").style.display = "";
        }
    }
    catch (err) {
        //alert(err);
    }
}