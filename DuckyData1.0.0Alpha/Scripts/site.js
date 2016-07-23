function BindAutocomplete(fieldID, webServiceUrl) {
    alert($("#" + fieldID).val());
    $( "#" + fieldID).autocomplete({
        source: function( request, response ) {
            $.ajax( {
                url: webServiceUrl,
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: '{ "searchTerm: "' + request.term + '"}',
                success: function( data ) {
                    var output = eval(data);
                    response($.map(output, function (item) {
                        var email = item.email;
                        return {
                            label: email,
                            value: email
                        }
                    }))
                }
            } );
        },
        minLength: 3,
        delay: 50,
        select: function( event, ui ) {
            $('#' + fieldID).change();
        }
    } );
}