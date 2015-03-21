function fancyTreeSelectSingleNodeLazyJs(selector,treeId,userId,boardId,echoActive,activeNode,argums,jsonData,forumUrl) 
{
    $('#' + treeId).fancytree(
            {
                title: 'Fancy Tree',                         
                toggleEffect: { height: 'toggle', duration: 100 },
        autoFocus: false,
    source: {
        url: jsonData + 's=' + boardId + argums + forumUrl

    },

    activate: function (event,dtnode) {
        $('#' + echoActive).text(dtnode.node.title);
        jQuery(selector).get(jsonData + '=-100' + '&active=' + dtnode.node.key, function(data) {});
    },
 
    lazyLoad: function (event, dtnode) {

        dtnode.result = $.ajax({
            url: jsonData + '=' + dtnode.node.key + argums + forumUrl,
            dataType: 'json' } );          
                         
    } 
} );

}

