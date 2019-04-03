// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


deleteAnyItem = (url, Id, successcallback) => {
    $('#deleteInput').val(Id);
    $('#delete-modal').attr('action', url);
    $('#delete-modal').modal('show');
    //Swal.fire({
    //    title: 'Are you sure?',
    //    text: "You won't be able to revert this!",
    //    type: 'warning',
    //    showCancelButton: true,
    //    confirmButtonColor: '#3085d6',
    //    cancelButtonColor: '#d33',
    //    confirmButtonText: 'Yes, delete it!'
    //}).then((result) => {
    //    if (result.value) {
    //        fetch(url, {
    //            method: 'post',
    //            headers: {
    //                "Content-Type":"application/json"
    //            },
    //            body: JSON.stringify({ Id })
    //        }).then(data => data.json()).then(result => {
    //            if (result) {
    //                Swal.fire({
    //                    title: 'Deleted!',
    //                    text: 'Your file has been deleted.',
    //                    type: 'success'
    //                }).then((result) => successcallback)
    //            } else {
    //                Swal.fire(
    //                    'Error!',
    //                    'Error',
    //                    'error'
    //                )
    //            }
    //        })
    //    }
    //})
}