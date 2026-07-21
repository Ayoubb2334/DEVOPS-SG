import { Component, OnInit } from '@angular/core';
import { SmartphoneService } from '../../core/services/smartphone.service';
import { Smartphone } from '../../core/models/smartphone.model';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {
  loading = true;
  totalProduits = 0;
  totalStock = 0;
  valeurTotale = 0;
  prixMoyen = 0;
  stockFaibleCount = 0;

  brandChartData: any;
  brandChartOptions: any;
  stockChartData: any;
  stockChartOptions: any;

  constructor(private smartphoneService: SmartphoneService) {}

  ngOnInit(): void {
    this.smartphoneService.getAll().subscribe({
      next: (data) => this.computeStats(data),
      error: () => { this.loading = false; }
    });
  }

  private computeStats(smartphones: Smartphone[]): void {
    this.totalProduits = smartphones.length;
    this.totalStock = smartphones.reduce((sum, s) => sum + s.stock, 0);
    this.valeurTotale = smartphones.reduce((sum, s) => sum + s.prix * s.stock, 0);
    this.prixMoyen = this.totalProduits ? smartphones.reduce((sum, s) => sum + s.prix, 0) / this.totalProduits : 0;
    this.stockFaibleCount = smartphones.filter(s => s.stock < 5).length;

    // Répartition par marque
    const brandMap = new Map<string, number>();
    smartphones.forEach(s => brandMap.set(s.marque, (brandMap.get(s.marque) || 0) + 1));

    const textColor = '#8b90ac';
    const gridColor = 'rgba(255,255,255,0.06)';

    this.brandChartData = {
      labels: Array.from(brandMap.keys()),
      datasets: [{
        data: Array.from(brandMap.values()),
        backgroundColor: '#6c5df0',
        hoverBackgroundColor: '#8b7bff',
        borderRadius: 8,
        maxBarThickness: 40
      }]
    };
    this.brandChartOptions = {
      plugins: { legend: { display: false } },
      scales: {
        x: { ticks: { color: textColor }, grid: { display: false } },
        y: { ticks: { color: textColor }, grid: { color: gridColor }, beginAtZero: true }
      }
    };

    // Répartition stock faible / normal
    const stockNormal = smartphones.filter(s => s.stock >= 5).length;
    this.stockChartData = {
      labels: ['Stock suffisant', 'Stock faible (<5)'],
      datasets: [{
        data: [stockNormal, this.stockFaibleCount],
        backgroundColor: ['#22d3c5', '#ff5c7a'],
        hoverBackgroundColor: ['#4fe3d6', '#ff8095'],
        borderWidth: 0
      }]
    };
    this.stockChartOptions = {
      plugins: { legend: { position: 'bottom', labels: { color: textColor } } },
      cutout: '65%'
    };

    this.loading = false;
  }
}