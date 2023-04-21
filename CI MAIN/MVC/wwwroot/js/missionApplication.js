$(document).ready(function () {

    $('#userTable').DataTable();
});



function approve(missionApplicationId) {
    $.ajax({
        url: '/Admin/Admin/approveMission',
        type: 'GET',
        data: { 'missionApplicationId': missionApplicationId},
        success: function (res) {            
            $("#missionApprove").click();
        },
        error: function (res) {
            console.log(res);
            alert("error");
        }
    });
}

function reject(missionApplicationId) {
    $.ajax({
        url: '/Admin/Admin/rejectMission',
        type: 'GET',
        data: { 'missionApplicationId': missionApplicationId },
        success: function (res) {
            $("#missionReject").click();
        },
        error: function (res) {
            console.log(res);
            alert("error");
        }
    });
}