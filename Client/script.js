
var app = new Vue({
    el: '#app',
    created() {
    },    
    mounted() {
    },
    data: {
      message: '',
      shouldShowNetworkError: false,
      searchQueryForCombindSearch: '',
      searchQueryForGoogle: '',
      searchQueryForBing: '',
      searchQueryForWiki: '',

      combindSearchResult: {searchQuery: '', formattedTotalHits: '', hits: null, queryWordResults: []},
      googleSearchResult: {searchQuery: '', formattedTotalHits: '', hits: null, queryWordResults:  []},
      bingSearchResult: {searchQuery: '', formattedTotalHits: '', hits: null, queryWordResults: []},
      wikiSearchResult: {searchQuery: '', formattedTotalHits: '', hits: null, queryWordResults: []},
      
      httpError: {message: ''},
      urls: {
        "combindSearch": 'https://localhost:7531/api/Search/AllSearch?query=',
        "googleSearch":  'https://localhost:7531/api/Search/GoogleSearch?query=',
        "bingSearch": 'https://localhost:7531/api/Search/BingSearch?query=',
        "wikiSearch": 'https://localhost:7531/api/Search/WikiSearch?query='
      },
    
    },
    methods: {

        searchInAllEngine() {
          let self = this;
          self.shouldShowNetworkError = false;
          if(self.searchQueryForCombindSearch){
            const url = `${self.urls['combindSearch']}${self.searchQueryForCombindSearch}`;
            axios.get(url).then((response) => {
              self.combindSearchResult = response.data;
            }).catch((error) => {
              self.handleHttpError(error);
            }).then(() => {
              self.searchQueryForCombindSearch = '';
            });                      
          }
        },

        searchInWiki() {
          let self = this;
          self.shouldShowNetworkError = false;
          if(self.searchQueryForWiki){
            const url = `${self.urls['wikiSearch']}${self.searchQueryForWiki}`;
            axios.get(url).then((response) => {
              self.wikiSearchResult = response.data;
            }).catch((error) => {
              self.handleHttpError(error);
            }).then(() => {
              self.searchQueryForWiki = '';
            });                      
          }
        },

        searchInGoogle() {
          let self = this;
          self.shouldShowNetworkError = false;
          if(self.searchQueryForGoogle){
            const url = `${self.urls['googleSearch']}${self.searchQueryForGoogle}`;
            axios.get(url).then((response) => {
              self.googleSearchResult = response.data;
            }).catch((error) => {
              self.handleHttpError(error);
            }).then(() => {
              self.searchQueryForGoogle = '';
            });                      
          }
        },

        searchInBing: function() {
          let self = this;
          self.shouldShowNetworkError = false;
          if(self.searchQueryForBing){
            const url = `${self.urls['bingSearch']}${self.searchQueryForBing}`;
            axios.get(url).then((response) => {
              self.bingSearchResult = response.data;
            }).catch((error) => {
              self.handleHttpError(error);
            }).then(() => {
              self.searchQueryForBing = '';
            });                      
          }
        },

        closeErrorView: function(){
          this.httpError.message = ''
          self.shouldShowNetworkError = false;
        },

        handleHttpError: function(error){
          if(error.code === "ERR_NETWORK"){
            this.shouldShowNetworkError = true;
          } else {
            this.httpError.message = error.message;
          } 
        }
      }    

  })