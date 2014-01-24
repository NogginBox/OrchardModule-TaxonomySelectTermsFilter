(function($) {
	$.fn.extend({
		SimpleSelectTree: function(options) {
			//var settings = $.extend({ speed: 3000, stay: 7000, items: 2 }, options);
			return this.each(function () {
				
				function tList(place, level, selectOptions, numOptions) {
					var terms = [];
					for (var i = place; i < numOptions; i++) {
						var currentLevel = indentLevel(selectOptions[i].text);
						if (currentLevel == level) {
							var text = selectOptions[i].text.substring(level * 2);
							terms.push({ value: selectOptions[i].value, text: text, selected: selectOptions[i].selected });
						}
						else if (currentLevel > level) {
							var termsIndex = length(terms) - 1;
							var subTerms = tList(i, currentLevel, selectOptions, numOptions);
							terms[termsIndex].terms = subTerms;
							terms[termsIndex].selected = terms[termsIndex].selected || anySelected(subTerms);
							i += subTerms.length - 1;
						}
						else {
							return terms;
						}

					}
					return terms;
				}

				function init(origSelect) {
					var newSelect = $("<select/>");
					newSelect.change(onSelectOption);
					populateTerms(allTerms, newSelect[0], 0);
					origSelect.before(newSelect);
					
					var currentIndex = firstSelected(allTerms);
					newSelect[0].selectedIndex = currentIndex;
					populateSubTermsFor(currentIndex, true);
				}
				
				function anySelected(terms) {
					return firstSelected(terms) != -1;
				}

				function firstSelected(terms) {
					var termsLength = length(terms);
					for (var i = 0; i < termsLength; i++) {
						if (terms[i].selected) return i;
					}
					return -1;
				}

				function indentLevel(text) {
					var textLength = text.length;
					for (var i = 0; i < textLength; i++) {
						if (text.charAt(i) != '-') return i / 2;
					}
					return 0;
				}

				function length(thing) {
					return (thing == null) ? 0 : thing.length;
				}

				function populateTerms(terms, toSelectBox, selectedIndex, allId) {
					toSelectBox.innerHTML = null;
					var termsLength = length(terms);

					if (allId > 0) {
						toSelectBox.add(new Option(allTerms[0].text, allId));
					}

					if (terms == null || termsLength == 0) {
						$(toSelectBox).hide();
						return;
					}
					else {
						$(toSelectBox).show();
					}
					
					
					for (var i = 0; i < termsLength; i++) {
						toSelectBox.add(new Option(terms[i].text, terms[i].value));
					}
					toSelectBox.selectedIndex = selectedIndex;
				}

				function onSelectOption(e) {
					populateSubTermsFor(e.currentTarget.selectedIndex, false);
				}

				function populateSubTermsFor(i, checkSelected) {
					selectbox[0].innerHTML = null;
					var subTerms = allTerms[i].terms;
					var subSelectedIndex = checkSelected
						? firstSelected(subTerms) + 1
						: 0;

					populateTerms(subTerms, selectbox[0], subSelectedIndex, allTerms[i].value);
				}

				var selectbox = $(this);
				var allTerms = tList(0, 0, selectbox[0], selectbox[0].length);
				init(selectbox);

				
				

			});
		}
	});
})(jQuery);