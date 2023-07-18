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
    name: 'Home',
    component: Home,
    //components: {
    //default: Home,
    //},
    meta: { scrollToTop: true },
    // props: {
    //   title: 'Heading',
    //   subTitle: 'Base Template',
    //   description: 'version: Sqeel alpha 0.0.1',
    // },
  
  
  },

  {
    path: '/bugaga',
    name: 'Counter',
    component: () => import('../vues/Table.vue'),
  },
  {
    path: '/:catchAll(.*)',
    component: NotFound,
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior,
})

export default router
