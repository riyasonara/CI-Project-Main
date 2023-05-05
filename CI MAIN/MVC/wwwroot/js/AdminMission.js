$(document).ready(function () {

    $('#userTable').DataTable();
});

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

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Admin/Admin/delmiss',
                type: 'GET',
                data: { 'missionId': missionId },
                success: function (res) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    console.log(res);
                    $("#User").click();
                }
            });


           
        }
    })



   
}






//=============================== theme ==============================


function theme() {
    var Title = document.getElementById("Title").value;
    var Status = document.getElementById("Status").value;
    var missionThemeId = document.getElementById("missionThemeId").value;

    $.ajax({
        url: '/Admin/Admin/theme',
        type: 'GET',
        data: { 'missionThemeId': missionThemeId, 'Title': Title, 'Status': Status },
        success: function (res) {
            $("#cms").click();
        },
        error: function (res) {
            console.log(res);
            alert("Modal error");
        }
    });
}

function editTheme(missionThemeId) {
    $.ajax({
        url: '/Admin/Admin/editTheme',
        type: 'GET',
        data: { 'missionThemeId': missionThemeId },
        success: function (res) {
            console.log(res);
            $("#Title").val(res.title);
            $("#Status").val(res.status);
            $("#missionThemeId").val(res.missionThemeId);
        },
        error: function (res) {
            console.log(res);
            alert("Modal error");
        }
    });
}


function nullvalues() {
    document.getElementById("Title").value = "";
    document.getElementById("Status").value = "";
    document.getElementById("missionThemeId").value = "";
}


function deleteTheme(missionThemeId) {

    $.ajax({
        url: '/Admin/Admin/deleteMissiontheme',
        type: 'GET',
        data: { 'missionThemeId': missionThemeId },
        success: function (res) {
            $("#missionReject").click();
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                }
            })
        },
        error: function (res) {
            console.log(res);
            alert("error");
        }
    });
}
