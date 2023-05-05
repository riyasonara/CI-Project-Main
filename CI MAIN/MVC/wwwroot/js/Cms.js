$(document).ready(function () {

    $('#userTable').DataTable();
});
var table = new DataTable('#userTable');

$('#myInputTextField').keyup(function () {
    table.search($(this).val()).draw();
});

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
                url: '/Admin/Admin/CMSDelete',
                type: 'GET',
                data: { 'CMSId': CMSId },
                success: function (res) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success',                     
                    )
                    console.log(res);
                },
            });
           
        }
    })
    
}

    function nullvalues() {
    document.getElementById("Title").value = "";
    document.getElementById("Desc").value = "";
    document.getElementById("Slug").value = "";
    document.getElementById("Status").value = "";
    document.getElementById("CMSId").value = "";
}
