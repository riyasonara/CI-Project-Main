

function adduser() {
    var Fname = $("#Fname").val();
    var Lname = $("#Lname").val();
    var email = $("#email").val();
    var password = $("#password").val();
    var empID = $("#empID").val();
    var department = $("#department").val();
    var country = $("#country").val();
    var city = $("#city").val();
    var proftxt = $("#proftxt").val();
    var status = $("#status").val();
    var userID = $("#userID").val();



    $.ajax({
        url: '/Admin/Admin/adduser',
        type: 'POST',
        data: { 'userID': userID, 'Fname': Fname, 'Lname': Lname, 'email': email, 'password': password, 'empID': empID, 'department': department, 'country': country, 'city': city, 'proftxt': proftxt, 'status': status },
        success: function (res) {
            location.reload();
            $('#myInputTextField').keyup(function () {
                table.search($(this).val()).draw();
        },
        error: function (res) {
            console.log(res);
            alert("Modal error");
        }
    });
}



function GetUser(userID) {
    $.ajax({
        url: '/Admin/Admin/GetUser',
        type: 'GET',
        data: { 'UserID': userID },
        success: function (res) {
            console.log(res);
            $("#userID").val(res.userID);
            $("#Fname").val(res.Fname);
            $("#Lname").val(res.Lname);
            $("#email").val(res.email);
            $("#password").val(res.password);
            $("#empID").val(res.empID);
            $("#department").val(res.department);
            $("#country").val(res.country);
            $("#city").val(res.city);
            $("#proftxt").val(res.proftxt);
            $("#status").val(res.status);
        },
        error: function (res) {
            console.log(res);
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

function nullvalues() {
    $("#userID").val('');
    $("#Fname").val('');
    $("#Lname").val('');
    $("#email").val('');
    $("#password").val('');
    $("#empID").val('');
    $("#department").val('');
    $("#country").val('');
    $("#city").val('');
    $("#proftxt").val('');
    $("#status").val('');
}