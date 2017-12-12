$(document).ready(function () {
    getProducts();
});

// Declare a variable to check when the action is Insert or Update
var isUpdateable = false;

// Get products list, by default, this function will be run first for the page load
function getProducts() {
    $.ajax({
        url: '/Genres/GetProducts/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var rows = '';
            $.each(data, function (i, item) {
                rows += "<tr>"

                rows += "<td>" + item.GenreName + "</td>"
               
                rows += "<td><button type='button' id='btnEdit' class='btn btn-default' onclick='return getProductById(" + item.GenreId + ")'>Edit</button> <button type='button' id='btnDelete' class='btn btn-danger' onclick='return deleteProductById(" + item.GenreId + ")'>Delete</button> <button type='button' id='btnDetails' class='btn btn-default' onclick='return getProductById2(" + item.GenreId + ")'>Details</button></td>"
                rows += "</tr>";
                $("#listProducts tbody").html(rows);
            });
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });
}

// Get product by id
function getProductById(id) {
    $("#title").text("Edit Genre");
    $.ajax({
        url: '/Genres/Get/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $("#GenreId").val(data.GenreId);
            $("#GenreName").val(data.GenreName);
           
            isUpdateable = true;
            $("#productModal").modal('show');
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });
}

function getProductById2(id) {
    $("#title2").text("Genre Details");
    $.ajax({
        url: '/Genres/Get/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $("#GenreId2").val(data.GenreId);
            $("#GenreName2").val(data.GenreName);
           
            isUpdateable = true;
            $("#productModal2").modal('show');
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });
}

// Insert/ Update a product
$("#btnSave").click(function (e) {

    var data = {
        GenreId: $("#GenreId").val(),
        GenreName: $("#GenreName").val()
       
    }

    if (!isUpdateable) {
        $.ajax({
            url: '/Genres/Create/',
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                getProducts();
                $("#productModal").modal('hide');
                clear();
            },
            error: function (err) {
                alert("Error: " + "Nie jesteś zalogowany jako Admin");
            }
        })
    }
    else {
        $.ajax({
            url: '/Genres/Update/',
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                getProducts();
                isUpdateable = false;
                $("#productModal").modal('hide');
                clear();
            },
            error: function (err) {
                alert("Error: " + "Nie jesteś zalogowany jako Admin");
            }
        })
    }
});

// Delete product by id
function deleteProductById(id) {
    $("#confirmModal #title").text("Delete Product");
    $("#confirmModal").modal('show');
    $("#confirmModal #btnOk").click(function (e) {
        $.ajax({
            url: "/Genres/Delete/" + id,
            type: "POST",
            dataType: 'json',
            success: function (data) {
                getProducts();
                $("#confirmModal").modal('hide');
            },
            error: function (err) {
                alert("Error: " + "Nie jesteś zalogowany jako Admin");
            }
        });

        e.preventDefault();
    });
}

// Set title for create new
$("#btnCreate").click(function () {
    $("#title").text("Create New");
})

// Close modal
$("#btnClose").click(function () {
    clear();
});

// Clear all items
function clear() {
    $("#GenreId").val("");
    $("#GenreName").val("");
   
}