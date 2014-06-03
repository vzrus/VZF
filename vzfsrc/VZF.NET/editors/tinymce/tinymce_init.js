tinyMCE.init({
	// General options
    selector: "textarea",
    language: editorLanguage,
    convert_urls: false,
    menu: {
        edit: { title: 'Edit', items: 'undo redo | cut copy paste | selectall' },
        insert: { title: 'Insert', items: '|' },
        view: { title: 'View', items: 'visualaid' },
        format: { title: 'Format', items: 'bold italic underline strikethrough superscript subscript | formats | removeformat' }
    },
});