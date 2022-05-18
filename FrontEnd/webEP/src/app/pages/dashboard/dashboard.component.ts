import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  empleos:any[] =[
    {
      titulo:"Buscamos a los mejores desarrolladores Backend: Ruby de Colombia.",
      salario:"3.000",
      prioridad:"Alta",
      tiempo_pub:"5 minutos",
      propuestas:"11",
      descripcion:"Solcito desarrollo de software para empresa de carga y encomiendas, el software debe contener los siguientes modulos: tablero, administrativo, guias, logistica, ventas, servicio al cliente."
    },
    {
      titulo:"Completar integracion de mi api con Whatsapp.",
      salario:"100",
      prioridad:"Normal",
      tiempo_pub:"1 hora",
      propuestas:"5",
      descripcion:"Ya esta funcionando pero el desarrollador que lo programo se desaparecio tengo todos los accesos a las herramientas utilizadas com firebase, google cloud, pickassist y otros."
    },
    {
      titulo:"Desarrollo en node.js.",
      salario:"35",
      prioridad:"Baja",
      tiempo_pub:"24 horas",
      propuestas:"1",
      descripcion:"Perfil para puestos de desarrollo en la nube. - Manejo de AWS profiles - EC2 / ECS - DocumentDB - Device API - Experiencia en node.js - Experiencia con rest api - experiencia con sensores y iot. - Experiencia con Docker y/o Kubernetes."
    },
    {
      titulo:"Diseño de Página Web.",
      salario:"300",
      prioridad:"Alta",
      tiempo_pub:"8 horas",
      propuestas:"43",
      descripcion:"Hola. Necesito a alguien me me ayude a diseñar una página web moderna, responsiva y que tenga un buen diseño. Interesados favor de enviarme propuesta y ejemplos de diseños propios."
    }
  ]

}
