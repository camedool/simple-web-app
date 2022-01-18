import Vue from "vue";
import App from "./App.vue";
import vuetify from "./plugins/vuetify";
import "roboto-fontface/css/roboto/roboto-fontface.css";
import "@mdi/font/css/materialdesignicons.css";
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";

Vue.config.productionTip = false;

Vue.use(Toast, {
	transition: "Vue-Toastification__bounce",
	position: "bottom-right",
	maxToasts: 20,
	newestOnTop: true,
});

new Vue({
	vuetify,
	render: (h) => h(App),
}).$mount("#app");
