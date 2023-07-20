import { createRouter, createWebHistory } from 'vue-router'

import Home from '../components/HelloWorld.vue'
import Table from '../vues/Table.vue'
import NotFound from '../vues/NotFound.vue'
import HelloUser from '../vues/HelloUser.vue'
import EnterVue from '../components/EnterVue.vue'
import Login from '../components/Login.vue'
import Measurer from '../components/Measurer.vue'

const scrollBehavior = (to, from, savedPosition) => {
  if (savedPosition) {
    return savedPosition
  } else {
    const position = {}
    if (to.hash) {
      position.selector = to.hash

      if (to.hash === '#anchor2') {
        position.offset = { y: 100 }
      }

      if (to.matched.some((m) => m.meta.scrollToTop)) {
        position.x = 0
        position.y = 0
      }
    }
    return position
  }
}

const routes = [
  /*
  {
    path: '/',
    component: HelloUser,
  },
*/
  {
    path: '/',
    component: EnterVue,
  },
  {
    path: '/Measurer',
    component: Measurer,
  },

  {
    path: '/Home',
    component: Home,
  },
  {
    path: '/Login/:role',
    name: 'Login',
    component: Login,
  },
  {
    path: '/:catchAll(.*)',
    component: NotFound,
  },
  /*
  {
    path: '/',
    component: HelloUser,
  },
  {
    path: '/table',
    component: Table,
  },
  {
    path: '/:catchAll(.*)',
    component: NotFound,
  },
  */
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior,
})

export default router
