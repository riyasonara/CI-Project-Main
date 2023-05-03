

function saveUser() {
    debugger;

    $.ajax({
        url: '/Admin/Admin/adduser',
        type: 'POST',
        data: {

            Fname: document.getElementById("Fname").value,
            Lname: document.getElementById("Lname").value,
            email: document.getElementById("email").value,
            password: document.getElementById("password").value,
            empID: document.getElementById("empID").value,
            department: document.getElementById("department").value,
            country: document.getElementById("country").value,
            city: document.getElementById("city").value,
            proftxt: document.getElementById("proftxt").value,
            status: document.getElementById("status").value,
            avatar: document.getElementById("user-profile-img").src,
        },
        success: function (res) {

            $('#myInputTextField').keyup(function () {
                table.search($(this).val()).draw();
                location.reload();
            })
        },
        error: function () {
            console.log(res);
            alert("Modal error");
        }
    });
}

function GetUser(id) {
    $.ajax({
        url: '/Admin/Admin/getUser',
        type: 'POST',
        data: {
            UserID: id
        },

        success: function (response) {           
            $("#AddModal").html($(response).find("#AddModal").html());
        },
        error: function () {
            console.log(response);
            alert("Modal error");
        }
    });
}

function deleteUser(userID) {
    $.ajax({
        url: '/Admin/Admin/deleteUser',
        type: 'GET',
        data: { 'UserID': userID },
        success: function (res) {
            swal("Are you sure you want to do this?", {
                buttons: ["Oh noez!", true],
            });
            console.log(res);
            $("#User").click();
        }
    });
}
function CHANGE(event) {
    const reader = new FileReader(); // Create a new FileReader object
    reader.onload = function () {
        document.getElementById('user-profile-img').src = reader.result; // Set the source of the image tag to the selected image
    }
    reader.readAsDataURL(event.target.files[0]); // Read the selected file as a data URL
}
function CLICK() {
    document.getElementById("imginput").click();
}