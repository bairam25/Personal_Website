function SetContextKey() {
    $find('aceSearch').set_contextKey($('#ddlSearchFilter').val());
}
function ProductsPopulated(sender) {
    var ul = document.createElement("UL");
     var Items = sender.get_completionList().childNodes;
    for (var i = 0; i < Items.length; i++) {
        var li = document.createElement('li');
        var anchor = document.createElement('a');
        //anchor.href = '#';
        var imgDiv = document.createElement('div');
        var spanDiv = document.createElement('div');
        var NameSpan = document.createElement('span');
        var CodeSpan = document.createElement('span');
        var CategorySpan = document.createElement('span');
        var SubCategorySpan = document.createElement('span');
     
        spanDiv.className = 'DescDiv';
        imgDiv.className = 'ace-img-wrapper';
        CategorySpan.className = 'badge badge-primary float-right';
        SubCategorySpan.className = 'badge badge-primary float-right';

        if (Items[i]._value != "") {
            var item = Items[i]._value;
            //if item value dosn`t include <> , this mean no Result Found , so don`t construnct the list and return
            if (!item.includes("<>")) {
                return;
            }
            //Get Values from Item string 
            var AR = item.split('<>');
            var Code = AR[0];
            var Name = AR[1];
            var Photo = AR[2];
            var Category = AR[3];
            var SubCategory = AR[4];

            var img = document.createElement('img');
            img.src = Photo;
            img.style.width = "50px";
            img.innerHTML = Name;
            //Set SKU code to lang to catch it in onClientItemSelected img click
            img.lang = Code;

            CodeSpan.innerHTML = Code;
            CodeSpan.id = "skuCode" + i;
            CodeSpan.className = "hidden";
            NameSpan.innerHTML = Name;
             //Set SKU code to lang to catch it in onClientItemSelected click
            NameSpan.lang = Code;

            CategorySpan.innerHTML = Category;
            SubCategorySpan.innerHTML = SubCategory;   
            spanDiv.appendChild(NameSpan);
            spanDiv.appendChild(CategorySpan);
            spanDiv.appendChild(SubCategorySpan);
            imgDiv.appendChild(img);

            anchor.href = window.location.origin + "/Item/" + Code;
            anchor.appendChild(imgDiv);
            anchor.appendChild(spanDiv);

            li.appendChild(anchor);
            
           
            ul.className = "autocomplete dropdown-menu";
            ul.appendChild(li);
            Items[i].innerHTML = '';
            Items[i].appendChild(li);
        }

    }

}
 