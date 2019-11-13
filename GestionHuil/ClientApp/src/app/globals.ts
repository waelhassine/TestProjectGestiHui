import { Injectable } from '@angular/core';

@Injectable()
export class Globals {
    isTechnicien = true;
    setCurrency(val) {
        this.isTechnicien = val;
    }

    getCurrency() {
        return this.isTechnicien;
    }
}
