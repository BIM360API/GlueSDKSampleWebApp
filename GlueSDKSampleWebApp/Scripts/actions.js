/* Copyright 2014 Autodesk, Inc.  All rights reserved.
Use of this software is subject to the terms of the Autodesk license agreement provided at the time of installation or download, or which otherwise accompanies this software in 
either electronic or hard copy form.   */

$(function () {

    // Function that makes an ajax request to a server endpoint that returns signing query parameter string required by the embedded viewer
    // Signatures are valid for a 5 minute window, so this should be called every time you pass a url to the embedded viewer
    function getSig() {
        var sig = "";
        $.ajax({
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            data: '{}',
            dataType: 'json',
            async: false,
            url: 'Actions.aspx/GetSigningParams',
            success: function (result) {
                if (result)
                    sig = result.d;
            }
        });
        return sig;
    }

    // Function that handles constructing the url for the embedded viewer iframe to load a selected action by action_id.  The action_id is stored
    // as a data attribute on the link element
    function clickLink() {
        var sig = getSig();
        var baseUrl = "https://b2-staging.autodesk.com/?" + sig + "&runner=embedded";
        if ($('#show_props').prop('checked'))
            baseUrl += "&modules=properties";
        var viewId = $(this).data("actionid");
        if (viewId == null || viewId.length === 0) return;
        var hashPath = "/#" + window.company + "/action/" + viewId;
        var finalUrl = baseUrl + hashPath;

        if (finalUrl !== null && finalUrl.length > 0)
            $('#iframe').attr('src', finalUrl);

        $('a.selected, li.selected').removeClass('selected');
        $(this).addClass('selected');
        $(this).parent().addClass('selected');

        return false;

    }

    // add click event handler for items in the Actions list
    var itemsList = $("#actionList");
    itemsList.find('li > a').on('click', clickLink);


    // add click event handler for showing the properties module
    $('#show_props').on('click', function () {
        itemsList.find('.selected').click();
    });


    // initialize the embedded viewer passing in the target iframe window
    GlueEmbedded.init(window.frames[0]);

    // on clicking Set Selection button, call setSelection on the embedded viewer to set the selected objects
    $('#set_selection').on('click', function () {
        var val = $('#selection').val();
        val = $.parseJSON(val);
        if (val)
            GlueEmbedded.setSelection(val);
    });

    // on clicking Get Properties, call getSelectedProperties on the embedded viewer to retrieve the properties for the currently selected object
    // (multi select is not supported for getSelectedProperties)
    // Or, to specify an object path, call getProperties on the embedded viewer and pass in an objectPath
    // Both calls require a "gotproperties" event handler for the return value
    $('#get_properties').on('click', function () {
        var val = $('#selection').val();
        if (!val || val.length == 0)
            GlueEmbedded.getSelectedProperties();
        else {
            val = $.parseJSON(val);
            if (val)
                GlueEmbedded.getProperties(val);
        }
    });

    // on clicking Zoom Selection, call zoomSelection on the embedded viewer to zoom into the already selected objects
    $("#zoom_selection").on('click', function () {
        GlueEmbedded.zoomSelection();
    });

    // specify an event handler on the document for the "selectionchanged" from the viewer. The event's eventData property will be set to the 
    // array of selected objects
    $(document).on('selectionchanged', function (e) {
        $("#messages").val("object selected: " + JSON.stringify(e.eventData));
    });

    // specify an event handler on the document for the "gotproperties" event from the viewr.  The event's eventData property will be set to the
    // collection of object properties
    $(document).on('gotproperties', function (e) {
        $("#messages").val("got properties: " + JSON.stringify(e.eventData));
    });
});