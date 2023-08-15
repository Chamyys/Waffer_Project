import { createRouter, createWebHistory } from 'vue-router'

import Home from '../components/HelloWorld.vue'
import Table from '../vues/Table.vue'
import NotFound from '../vues/NotFound.vue'
import HelloUser from '../vues/HelloUser.vue'
import EnterVue from '../components/EnterVue.vue'
import Login from '../components/Login.vue'
import Measurer from '../components/Measurer.vue'
import Technologist from '../components/Technologist.vue'
import Welcomeback from '@/components/CreateWelcomeBack.vue'
import Admin from '../components/Admin/Admin.vue'
import AdminChooseConfig from '@/components/Admin/AdminChooseConfig.vue'
import AddUser from '@/components/Admin/AddUser.vue'
import Delete from '@/components/Admin/Delete.vue'
import Edit from '@/components/Admin/EditUser.vue'
import AddNewMT from '@/components/AddNewMT.vue'
import Missions from '@/components/Missions.vue'
import CreateMonitor from '@/components/CreateMonitor.vue'
import WaferTimeLine from '@/components/WaferTimeLine.vue'
import CreateLabEntry from '@/components/CreateLabEntry.vue'
import axios from 'axios'

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
    meta: { requiresMeasurer: true },
    children: [
      {
        path: '/Measurer/WelcomeBack',
        name: 'MeasurerWelcomeback',
        component: Welcomeback,
      },
      {
        path: '/Measurer/LabEntry',
        name: 'MeasurerCreateLabEntry',
        component: CreateLabEntry,
      },
      {
        path: '/Measurer/Missions',
        name: 'MeasurerMissions',
        component: Missions,
      },
      {
        path: '/Measurer/CreateMonitor',
        name: 'MeasurerCreateMonitor',
        component: CreateMonitor,
      },
      {
        path: '/Measurer/WaferTimeLine',
        name: 'MeasurerWaferTimeLine',
        component: WaferTimeLine,
      },
    ],
  },
  {
    path: '/Technologist',
    component: Technologist,
    meta: { requiresTechnologist: true },
    children: [
      {
        path: '/Technologist/WaferTimeLine',
        name: 'MeasurerWaferTimeLine',
        component: WaferTimeLine,
      },
      {
        path: '/Technologist/WelcomeBack',
        name: 'TechnologistWelcomeback',
        component: Welcomeback,
      },
      {
        path: '/Technologist/Missions',
        name: 'TechnologistMissions',
        component: Missions,
      },
      {
        path: '/Technologist/AddNewMT',
        name: 'AddNewMT',
        component: AddNewMT,
      },
      {
        path: '/Technologist/WaferTimeLine',
        name: 'TechnologistWaferTimeLine',
        component: WaferTimeLine,
      },
    ],
  },
  {
    path: '/Admin',
    component: Admin,
    meta: { requiresAdmin: true },
    children: [
      {
        path: '/Admin/AdminChooseConfig',
        name: 'AdminChooseConfig',
        component: AdminChooseConfig,
        children: [
          {
            path: '/Admin/AdminChooseConfig/Delete',
            name: 'Delete',
            component: Delete,
          },
          {
            path: '/Admin/AdminChooseConfig/Edit',
            name: 'Edit',
            component: Edit,
          },
          {
            path: '/Admin/AdminChooseConfig/AddUser',
            name: 'AddUser',
            component: AddUser,
          },
        ],
      },
    ],
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
/*
const postMission = async () => {
  try {
    const response = await axios.post(
      'https://localhost:3000/api/WaferCreateMission/Post',
      createMission()
    )

    console.log(response)
  } catch (error) {
    console.error(error)
  }
}
*/
router.beforeEach((to, from, next) => {
  const requiresAdmin = to.matched.some((record) => record.meta.requiresAdmin)
  const requiresMeasurer = to.matched.some(
    (record) => record.meta.requiresMeasurer
  )
  const requiresTechnologist = to.matched.some(
    (record) => record.meta.requiresTechnologist
  )

  const userRole = localStorage.getItem('role') // Получение роли пользователя из localstorage - переделать на запрос к серверу

  if (requiresAdmin && userRole !== 'Admin') {
    next('/')
  } else if (requiresMeasurer && userRole !== 'Measurer') {
    next('/')
  } else if (requiresTechnologist && userRole !== 'Technologist') {
    next('/')
  } else {
    next()
  }
})

export default router
