function fancytreeGetNodesAdminLazyJs(selector, treeId, userId, boardId, impossibleLabel, argums, jsonData, forumPath, menuPoints) {
   
    var menuItems = {};
    menuItems = JSON.parse(menuPoints);

    $('#' + treeId).fancytree(
    {
        extensions: ['dnd'],
        title: 'Fancy Tree',
        toggleEffect: { height: 'toggle', duration: 200 },
        autoFocus: false,
        source: {
            url: jsonData + '?tjls=' + boardId + forumPath
        },
        dnd: {
            autoExpandMS: 500,
            focusOnClick: true,
            preventVoidMoves: true,
            preventRecursiveMoves: true,
            dragStart: function(node, data) {
                return true;
            },
            dragEnter: function(node, data) {
                if (node.folder == true && data.otherNode.folder) {
                    return ['before', 'after'];
                };
                if ((node.folder == true && data.otherNode.folder == false)) {
                    return ['over'];
                };
                if (node.folder == false && data.otherNode.folder == true) {
                    return false;
                };
                return true;
            },
            dragDrop: function(node, data) {
                var saveready;
                var movepath;
                data.otherNode.moveTo(node, data.hitMode);
                //// console.log(' hit mode = ' + data.hitMode + ';'  );
                //// console.log(' other node title =' + data.otherNode.title + '; key=' +  data.otherNode.key );
                //// console.log(' node title =' + node.title + '; key=' +  data.otherNode.key );
                //// console.log(' node parent title =' + data.node.parent.title + '; key' +  data.node.parent.key );
                //// console.log(' othernode parent title =' + data.otherNode.parent.title + '; key' +  data.otherNode.parent.key );
                //// moving as a forum child
                movepath = jsonData + '?trno=' + data.otherNode.key
                    + '&trn=' + data.node.key
                    + '&trnp=' + data.node.parent.key
                    + '&trnop=' + data.otherNode.parent.key
                    + '&trna=' + data.hitMode + argums + forumPath;


                saveready = $.ajax({
                    url: movepath,
                    dataType: 'text'
                });
                console.log(' ready = ' + saveready + ';');
            }
        },
        activate: function (event, data) {
        },
        lazyLoad: function(event, dtnode) {
            dtnode.result = $.ajax({
                url: jsonData + '?tjl=' + dtnode.node.key + argums + forumPath,
                dataType: 'json'
            });
        }
    });

    $('#' + treeId).contextmenu({
        delegate: 'span.fancytree-title',
        ////      menu: '#options',
        menu: [
            { title: menuItems["delete"], cmd: 'delete', uiIcon: 'ui-icon-trash', disabled: false },
            { title: '----' },
            { title: menuItems.edit, cmd: 'edit', uiIcon: 'ui-icon-pencil', disabled: false },
            {
                title: menuItems.new,
                data: 'new',
                uiIcon: 'ui-icon-plus',
                children: [
                    {
                        title: menuItems["category"],
                        data: 'category',
                        uiIcon: 'ui-icon-folder-collapsed',
                        children: [
                            { title: menuItems["before"], cmd: 'catbefore', uiIcon: 'ui-icon-arrow-1-n', disabled: false },
                            { title: menuItems["after"], cmd: 'catafter', uiIcon: 'ui-icon-arrow-1-s', disabled: false }
                        ]
                    },
                    {
                        title: menuItems["forum"],
                        data: 'forum',
                        uiIcon: 'ui-icon-note',
                        children: [
                            { title: menuItems["before"], cmd: 'frmbefore', uiIcon: 'ui-icon-arrow-1-n', disabled: false },
                            { title: menuItems["after"], cmd: 'frmafter', uiIcon: 'ui-icon-arrow-1-s', disabled: false },
                            { title: menuItems["over"], cmd: 'frmover', uiIcon: 'ui-icon-arrowreturn-1-e', disabled: false }
                        ]
                    }
                ]
            }
        ],
        beforeOpen: function(event, ui) {
            var node = $.ui.fancytree.getNode(ui.target);
            console.log(' node.folder = ' + node.folder + ';');
            if (!node.folder) {
                $('#' + treeId).contextmenu('setEntry', 'new',
                    {
                        title: menuItems["new"],
                        data: 'new',
                        uiIcon: 'ui-icon-plus',
                        children: [
                            {
                                title: menuItems["forum"],
                                data: 'forum',
                                uiIcon: 'ui-icon-note',
                                children: [
                                    { title: menuItems["before"], cmd: 'frmbefore', uiIcon: 'ui-icon-arrow-1-n', disabled: false },
                                    { title: menuItems["after"], cmd: 'frmafter', uiIcon: 'ui-icon-arrow-1-s', disabled: false },
                                    { title: menuItems["over"], cmd: 'frmover', uiIcon: 'ui-icon-arrowreturn-1-e', disabled: false }
                                ]
                            }
                        ]
                    }
                );
            } else {
                $('#' + treeId).contextmenu('setEntry', 'newm',
                {
                    title: menuItems["new"],
                    uiIcon: 'ui-icon-plus',
                    data: 'new',
                    children: [
                        {
                            title: menuItems["category"],
                            data: 'category',
                            uiIcon: 'ui-icon-folder-collapsed',
                            children: [
                                { title: menuItems["before"], cmd: 'catbefore', uiIcon: 'ui-icon-arrow-1-n', disabled: false },
                                { title: menuItems["after"], cmd: 'catafter', uiIcon: 'ui-icon-arrow-1-s', disabled: false }
                            ]
                        }
                    ]

                });
            };
            node.setActive();
        },
        select: function(event, ui) {
            var node = $.ui.fancytree.getNode(ui.target);
            var url = '';

            //// alert('select ' + ui.cmd + ' on ' + node);

            switch (ui.cmd) {
            case 'edit':
                if (node.folder) {
                    url = url + 'g=admin_editcategory&c=' + node.key;
                } else {
                    url = url + 'g=admin_editforum&fa=' + node.key;
                };
                break;
            case 'delete':
                if (node.folder) {
                    url = url + 'g=admin_deletecategory&fa=' + node.key;
                } else {
                    url = url + 'g=admin_deleteforum&fa=' + node.key;
                };
                break;

            case 'catbefore':
                if (node.folder) {
                    url = url + 'g=admin_editcategory&before=' + node.key;
                } else {
                    alert(impossibleLabel);
                };
                break;
            case 'catafter':
                if (node.folder) {
                    url = url + 'g=admin_editcategory&after=' + node.key;
                } else {
                    alert(impossibleLabel);
                };
                break;
            case 'frmbefore':
                if (!node.folder) {
                    url = url + 'g=admin_editforum&before=' + node.key;
                } else {
                    alert(impossibleLabel);
                };
                break;
            case 'frmafter':
                if (!node.folder) {
                    url = url + 'g=admin_editforum&after=' + node.key;
                } else {
                    alert(impossibleLabel);
                };
                break;
            case 'frmover':
                url = url + 'g=admin_editforum&over=' + node.key;

                break;
            };


            if (url.length > 0) {
                url = '/default.aspx?' + url;
                if (navigator.userAgent.match(/MSIE\s(?!9.0)/)) {
                    var referLink = document.createElement('a');
                    referLink.href = url;
                    document.body.appendChild(referLink);
                    referLink.click();
                } else {
                    window.location.replace(url);
                };
            };
        }
    });

}