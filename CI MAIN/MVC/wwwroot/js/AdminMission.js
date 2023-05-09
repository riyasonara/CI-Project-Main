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



function themeadd() {
    $.ajax({
        url: '/Admin/Admin/addTheme',
        type: 'POST',
        data: {
            themeName: document.getElementById("themeName").value,
            Status: document.getElementById("Status").value,
            missionThemeId: document.getElementById("missionThemeId").value,
        },
        success: function (res) {
            console.log(res);
            $("#themeModal").html($(res).find("#themeModal").html());
        },
        error: function (res) {
            console.log(res);
            alert("Modal error");
        }
    });
}

function getTheme(missionThemeId) {
    $.ajax({
        url: '/Admin/Admin/Themeget',
        type: 'POST',
        data: {
            missionThemeId: missionThemeId
        },

        success: function (response) {
            $("#themeModal").html($(response).find("#themeModal").html());
        },
        error: function () {
            console.log(response);
            alert("Modal error");
        }
    });
}

function themedelete(missionThemeId) {

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
                url: '/Admin/Admin/deltheme',
                type: 'GET',
                data: { 'missionThemeId': missionThemeId },
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



//==================================================== cascading =========================================

filteredCitites();
function filteredCitites() {
    //alert("hiiii");
    var missionCityDiv = document.getElementById("city");
    var missionCountry = document.getElementById("country").value;
    var missioncity = document.getElementById("city");

    //alert(missionCountry)
    while (missionCityDiv.hasChildNodes()) {
        missionCityDiv.removeChild(missionCityDiv.firstChild);
    }

    $.ajax({
        /* url: '@Url.Action("filterCity", "UserProfile")',*/
        url: '/Admin/Admin/filterCity',
        type: 'GET',
        data: { missionCountry },
        datatype: "json",
        success: function (result) {
            console.log(result)
            result.map((city, index) => {
                console.log(city)
                var newCityOption = document.createElement('option');
                newCityOption.value = city.cityId;
                newCityOption.innerText = city.name;
                console.log(newCityOption)
                missioncity.appendChild(newCityOption)
            })
        }
    });
}





