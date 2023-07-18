import { createRouter, createWebHistory } from 'vue-router'

import Home from '../components/HelloWorld.vue'
import Table from '../vues/Table.vue'
import NotFound from '../vues/NotFound.vue'
import HelloUser from '../vues/HelloUser.vue'
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
  {
    path: '/',
    component: HelloUser,
  },
  {
    path: '/Home',
    component: Home,
  },
  {
    path: '/Table/:btnnumber',
    name: 'Table',
    component: Table,
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
