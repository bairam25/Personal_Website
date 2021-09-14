function Rating(sender) {
    try {
        var RateValue = $(sender).rateit('value');
        //alert(RateValue);
        var ItemId = $(sender).attr('itemid');
        WebService.SetRating(RateValue, ItemId, function (val) {
            $(sender).rateit('value', val);
            //alert(val);
        });
    }
    catch (err) {
        alert(err);
    }
}