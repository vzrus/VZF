function fancyTreeSetNodesGroupAccessLazyJS(selector, treeId, userId, boardId, groupId, argums, initializeArguments, jsonData, forumUrl) {
    $('#' + treeId).fancytree(
    {
        title: 'Fancy Tree',
        toggleEffect: { height: 'toggle', duration: 200 },
        autoFocus: false,
        checkbox: false,
        source: {
            url: jsonData + 'tjls=' + boardId + initializeArguments + argums + forumUrl
        },
        activate: function(event, data) {
            ////  $('#{6}').value = data.node.key;
            var node = data.node;
            if (data.node.href) {
                window.open(node.data.href, node.data.target);
            }
        },

        lazyLoad: function(event, dtnode) {
            dtnode.result = $.ajax({
                url: jsonData + 'tjl=' + dtnode.node.key + argums + forumUrl,
                dataType: 'json'
            });
        }
    });

    $('#' + treeId).delegate('select[name=selectaccessddl]', 'change', function(e) {
        var node = $.ui.fancytree.getNode(e),
            $select = $(e.target);
        e.stopPropagation(); //// prevent fancytree activate for this row
        $.ajax({
            url: jsonData + 'fgacc=' + $select.val() + '&fid=' + node.key + '&gid=' + groupId,
            dataType: 'json'
        });

        ////   if($select.is(':selected')){
        ////      alert('selectaccessddl ' + $select.val());
        ////   }else{
        ////    alert('dislike ' + $select.val());
        ////    }

    });
}