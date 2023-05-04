$(document).ready(function () {

    $('#userTable').DataTable();
});


//$(document).ready(function () {
//    var myDropzone = new Dropzone("#my-dropzone", {
//        paramName: "file",
//        maxFilesize: 2, // MB
//        addRemoveLinks: true,
//        dictRemoveFile: "Remove"
//    });

//    myDropzone.on("success", function (file, response) {
//        // Handle the server response here
//    });
//});


function missionadd() { 
    $.ajax({
        url: '/Admin/Admin/addMission',
        type: 'POST',
        data: {
            Title: document.getElementById("Title").value,
            ShortDesc: document.getElementById("ShortDesc").value,
            Desc: document.getElementById("Desc").value,
            city: document.getElementById("city").value,
            country: document.getElementById("country").value,
            OrgName: document.getElementById("OrgName").value,
            OrgDetail: document.getElementById("OrgDetail").value,
            misstype: document.getElementById("misstype").value,
            seats: document.getElementById("seats").value,
            startdate: document.getElementById("startdate").value,
            endDate: document.getElementById("endDate").value,
            RegDeadline: document.getElementById("RegDeadline").value,
            availability: document.getElementById("availability").value,
            themeid: document.getElementById("themeid").value,
            skill: document.getElementById("skill").value,
            missionId: document.getElementById("missionId").value,
        },
        success: function (res) {
            console.log(res);
            $("#missionModal").html($(res).find("#missionModal").html());
           /* $("#cms").click();*/
        },
        error: function (res) {
            console.log(res);
            alert("Modal error");
        }
    });
}

function getMission(missionId) {
    $.ajax({
        url: '/Admin/Admin/getMiss',
        type: 'POST',
        data: {
            missionId: missionId
        },

        success: function (response) {
            $("#missionModal").html($(response).find("#missionModal").html());
        },
        error: function () {
            console.log(response);
            alert("Modal error");
        }
    });
}

function missiondelete(missionId) {
    $.ajax({
        url: '/Admin/Admin/delmiss',
        type: 'GET',
        data: { 'missionId': missionId },
        success: function (res) {
            swal("Are you sure you want to do this?", {
                buttons: ["Oh noez!", true],
            });
            console.log(res);
            $("#User").click();
        }
    });
}