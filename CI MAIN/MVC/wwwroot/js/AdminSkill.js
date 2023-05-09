﻿//=============================== Skill ==============================


function skilladd() {
    $.ajax({
        url: '/Admin/Admin/addskill',
        type: 'POST',
        data: {
            skillName: document.getElementById("skillName").value,
            Status: document.getElementById("Status").value,
            skillId: document.getElementById("skillId").value,
        },
        success: function (res) {
            console.log(res);
            $("#skillModal").html($(res).find("#skillModal").html());
        },
        error: function (res) {
            console.log(res);
            alert("Modal error");
        }
    });
}

function getSkill(skillId) {
    $.ajax({
        url: '/Admin/Admin/skillget',
        type: 'POST',
        data: {
            skillId: skillId
        },

        success: function (response) {
            $("#skillModal").html($(response).find("#skillModal").html());
        },
        error: function () {
            console.log(response);
            alert("Modal error");
        }
    });
}

function skilldelete(skillId) {

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
                url: '/Admin/Admin/delskill',
                type: 'GET',
                data: { 'skillId': skillId },
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