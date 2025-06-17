// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var firstName = $('#firstName').text();
    var lastName = $('#lastName').text();
    var intials = $('#firstName').text().charAt(0);
    var profileImage = $('.profileImage').text(intials);
});

deleteAnyItem = (url, Id, successcallback) => {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            fetch(url, {
                method: 'post',
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({Id})
            }).then(data => data.json()).then(result => {
                if (result) {
                    Swal.fire({
                        title: 'Deleted!',
                        text: 'Your file has been deleted.',
                        type: 'success'
                    }).then((result) => location.reload());
                } else {
                    Swal.fire(
                        'Error!',
                        'Error',
                        'error'
                    );
                }
            });
        }
    });
};

editAnyItem = (current) => {
    const {id, url, form} = current.dataset;

    fetch(url, {
        method: 'post',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({Id: id})
    }).then(res => res.json()).then(res => {
        Object.keys(res).map(key => $('#' + form + '_' + key).val(res[key]));

        $('#' + form).modal('show');
    });
};

searchHtml = (current) => {
    var filter, table, tr, td, i, txtValue;
    filter = current.value.toUpperCase();

    const {tableid} = current.dataset;

    table = document.getElementById(tableid);
    tr = table.getElementsByClassName("searchDiv");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("h5")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
};

SharedAdd = (current) => {
    const {type, name, modal, title} = current.dataset;

    var modalForm = $('#' + modal);
    modalForm.modal('hide');

    Swal.fire({
        title: 'Submit your ' + title,
        input: 'text',
        showCancelButton: true,
        confirmButtonText: 'Save',
        showLoaderOnConfirm: true,
        preConfirm: (text) => {
            return fetch('/Shared/Add', {
                method: 'post',
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({Name: text, Type: type})
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error(response.statusText);
                    }
                    return response.json();
                }).then((result) =>
                    refreshSelect(name, result))
                .catch(error => {
                    Swal.showValidationMessage(
                        error
                    );
                });
        },
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                title: 'Success!',
                text: 'Has been Added.',
                type: 'success'
            }).then((result) =>
                modalForm.modal('show'));
        } else {
            modalForm.modal('show');
        }
    });
};
refreshSelect = (name, data) => {
    var select = $('[name="' + name + '"]');
    select.empty();
    data.map(item => select.append(`<option value="${item.id}">${item.ad}</option>`));
};