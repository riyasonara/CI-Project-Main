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
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Mission is Approved',
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

function reject(missionApplicationId) {

    $.ajax({
        url: '/Admin/Admin/rejectMission',
        type: 'GET',
        data: { 'missionApplicationId': missionApplicationId },
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