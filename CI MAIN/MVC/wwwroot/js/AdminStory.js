﻿$(document).ready(function () {

    $('#userTable').DataTable();
});



function approve(StoryId) {
    $.ajax({
        url: '/Admin/Admin/approveStory',
        type: 'GET',
        data: { 'StoryId': StoryId },
        success: function (res) {
            $("#missionApprove").click();
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Story is Approved',
                showConfirmButton: false,
                timer: 3000
            })
        },
        error: function (res) {
            console.log(res);
            alert("error");
        }
    });
}

function reject(StoryId) {
    $.ajax({
        url: '/Admin/Admin/rejectStory',
        type: 'GET',
        data: { 'StoryId': StoryId },
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