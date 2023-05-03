var dashboadtime = document.getElementById("dashboadtime");
function currentTime() {
    var time = new Date();
    var dateString = time.toDateString();
    var timeString = time.toLocaleTimeString();
    dashboadtime.innerHTML = dateString + ' ' + timeString;
    let t = setTimeout(function () { currentTime() }, 1000);
}
currentTime();


function ved1() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    var b = a.options[a.selectedIndex];
    for (var i = 0; i < a.length; i++) {

        if (a.options[i].selected == true) {
            a.options[i].selected = false
            c.add(a.options[i])

            ved1()
        }

    }
}
function ved2() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    var b = c.options[c.selectedIndex];
    for (var i = 0; i < c.length; i++) {
        if (c.options[i].selected == true) {
            c.options[i].selected = false
            a.add(c.options[i])
            ved2()
        }
    }
}
function ved3() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    for (var i = 0; i < a.length;) {
        c.add(a.options[c, i])
    }
}
function ved4() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    for (var i = 0; i < c.length;) {
        a.add(c.options[a, i])
    }
}

$(document).ready(function () {

    $('#userTable').DataTable();
});



//============================================================= cms =============================================================

function AddEdit() {
    var Title = document.getElementById("Title").value;
    var Desc = document.getElementById("Desc").value;
    var Slug = document.getElementById("Slug").value;
    var Status = document.getElementById("Status").value;
    var CMSId = document.getElementById("CMSId").value;

    $.ajax({
        url: '/Admin/Admin/CMSAdd',
        type: 'POST',
        data: { 'CMSId': CMSId, 'Title': Title, 'Desc': Desc, 'Slug': Slug, 'Status': Status },
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

function GetCmsData(CMSId) {
    $.ajax({
        url: '/Admin/Admin/GetCMSData',
        type: 'GET',
        data: { 'CMSId': CMSId },
        success: function (res) {
            console.log(res);
            //$("#UserModal").html($(res).find("#UserModal").html());
            $("#Title").val(res.title);
            $("#Desc").val(res.description);
            $("#Slug").val(res.slug);
            $("#Status").val(res.status);
            $("#CMSId").val(res.cmsPageId);
        },
        error: function (res) {
            console.log(res);
            alert("Modal error");
        }
    });
}

function DeleteCMS(CMSId) {
    $.ajax({
        url: '/Admin/Admin/CMSDelete',
        type: 'GET',
        data: { 'CMSId': CMSId },
        success: function (res) {
            $("#cms").click();
            window.location.reload();
        },
        error: function (res) {
            $("#cms").click();
        }
    });
}

function nullvalues() {
    document.getElementById("Title").value = "";
    document.getElementById("Desc").value = "";
    document.getElementById("Slug").value = "";
    document.getElementById("Status").value = "";
    document.getElementById("CMSId").value = "";
}

//CKEDITOR.replace('editor1');

