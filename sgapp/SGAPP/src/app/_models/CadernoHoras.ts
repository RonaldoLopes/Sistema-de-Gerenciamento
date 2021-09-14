import { Time } from '@angular/common';

export interface CadernoHoras {

    id: number;
    data: Date;
    horasDia: Time;
    deslocamento: Time;
    horasTrab: Time;
    projetosId: number;
    userId: number;
    userName: string;
}

