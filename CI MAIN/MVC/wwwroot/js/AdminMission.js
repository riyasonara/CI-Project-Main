$(document).ready(function () {

    $('#userTable').DataTable();
});


$(document).ready(function () {
    var myDropzone = new Dropzone("#my-dropzone", {
        paramName: "file",
        maxFilesize: 2, // MB
        addRemoveLinks: true,
        dictRemoveFile: "Remove"
    });

    myDropzone.on("success", function (file, response) {
        // Handle the server response here
    });
});


function addMission() {
    var Title = document.getElementById("Title").value;
    var ShortDesc = document.getElementById("ShortDesc").value;
    var Desc = document.getElementById("Desc").value;
    var city = document.getElementById("city").value;
    var country = document.getElementById("country").value;
    var OrgName = document.getElementById("OrgName").value;
    var OrgDetail = document.getElementById("OrgDetail").value;
    var misstype = document.getElementById("misstype").value;
    var seats = document.getElementById("seats").value;
    var dat = document.getElementById("dat").value;
    var RegDeadline = document.getElementById("RegDeadline").value;
    var availability = document.getElementById("availability").value;
    var themeid = document.getElementById("themeid").value;
    var skill = document.getElementById("skill").value;
    var missionId = document.getElementById("missionId").value;
    debugger;
    $.ajax({
        url: '/Admin/Admin/addMission',
        type: 'POST',
        data: { 'Title': Title, 'ShortDesc': ShortDesc, 'Desc': Desc, 'city': city, 'country': country, 'OrgName': OrgName, 'OrgDetail': OrgDetail, 'misstype': misstype, 'seats': seats, 'dat': dat, 'RegDeadline': RegDeadline, 'availability': availability, 'themeid': themeid, 'skill': skill, 'missionId': missionId },
        success: function (res) {
            //console.log(res);
            //$("#UserModal").html($(res).find("#UserModal").html());
            $("#cms").click();
        },
        error: function (res) {
            console.log(res);
            alert("Modal error");
        }
    });
}