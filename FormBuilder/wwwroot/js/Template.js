document.addEventListener("DOMContentLoaded", function () {
    const tagInput = document.getElementById('tagInput');
    const suggestionsBox = document.getElementById('tagSuggestions');
    const addedTagsContainer = document.getElementById('addedTags');

    // Store tags here
    let addedTags = [];

    function renderTags() {
        addedTagsContainer.innerHTML = '';
        addedTags.forEach((tag, index) => {
            const tagElem = document.createElement('div');
            tagElem.className = "flex items-center bg-emerald-100 text-emerald-800 rounded-full px-3 py-1 text-sm";
            tagElem.innerHTML = `
                <span>${tag}</span>
                <button type="button" class="ml-2 font-bold focus:outline-none" aria-label="Remove tag">&times;</button>
            `;
            tagElem.querySelector('button').addEventListener('click', () => {
                addedTags.splice(index, 1);
                renderTags();
            });
            addedTagsContainer.appendChild(tagElem);
        });

        // Update hidden inputs for form submission
        // Remove existing hidden inputs first
        document.querySelectorAll('input[name="Tags"]').forEach(e => e.remove());
        addedTags.forEach(tag => {
            const hiddenInput = document.createElement('input');
            hiddenInput.type = 'hidden';
            hiddenInput.name = 'Tags';
            hiddenInput.value = tag;
            document.querySelector('form').appendChild(hiddenInput);
        });
    }

    function addTag(tag) {
        tag = tag.trim();
        if (tag.length > 0 && !addedTags.includes(tag)) {
            addedTags.push(tag);
            renderTags();
        }
        tagInput.value = '';
        suggestionsBox.classList.add('hidden');
    }

    tagInput.addEventListener('input', function () {
        const query = this.value.trim();
        if (query.length === 0) {
            suggestionsBox.innerHTML = '';
            suggestionsBox.classList.add('hidden');
            return;
        }

        fetch(`/Template/AutoCompleteTags?keyWord=${encodeURIComponent(query)}`)
            .then(response => response.json())
            .then(tags => {
                if (tags.length === 0) {
                    suggestionsBox.innerHTML = '<div class="p-2 text-gray-500">No tags found</div>';
                } else {
                    suggestionsBox.innerHTML = tags.map(tag =>
                        `<div class="p-2 cursor-pointer hover:bg-emerald-200">${tag}</div>`
                    ).join('');
                }
                suggestionsBox.classList.remove('hidden');

                suggestionsBox.querySelectorAll('div').forEach(item => {
                    item.addEventListener('click', function () {
                        addTag(this.textContent);
                    });
                });
            })
            .catch(err => {
                console.error('Error fetching tags:', err);
                suggestionsBox.classList.add('hidden');
            });
    });

    // Add tag on Enter key
    tagInput.addEventListener('keydown', function (e) {
        if (e.key === 'Enter') {
            e.preventDefault();
            if (tagInput.value.trim().length > 0) {
                addTag(tagInput.value);
            }
        }
    });

    // Hide suggestions if clicking outside
    document.addEventListener('click', function (e) {
        if (!suggestionsBox.contains(e.target) && e.target !== tagInput) {
            suggestionsBox.classList.add('hidden');
        }
    });
});
