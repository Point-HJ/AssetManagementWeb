/// <reference path="../typings/jquery/jquery.d.ts" />
var AssingnLocationModel = /** @class */ (function () {
    function AssingnLocationModel() {
    }
    return AssingnLocationModel;
}());
function initAssetAssigmnet() {
    $("#AssignAssetButton").click(function () {
        var locationCode = $("#LocationCode").val();
        var assetCode = $("#AssetCode").val();
        alert("L: " + locationCode + ", A: " + assetCode);
        var data = new AssingnLocationModel();
        data.LocationCode = locationCode;
        data.AssetCode = assetCode;
        //lähetetään JSON-muotoista dataa palvelimelle
        $.ajax({
            type: "POST",
            url: "/Asset/AssignLocation",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (data.success == true) {
                    alert("Asset successfully assigned.");
                }
                else {
                    alert("There was an error: " + data.error);
                }
            },
            dataType: "json"
        });
    });
}
//# sourceMappingURL=Logic.js.map