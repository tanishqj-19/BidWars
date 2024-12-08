import jsPDF from 'jspdf';
import 'jspdf-autotable';
import { Player } from '../../models/Player';
import { Contract } from '../../models/Contract';


export class BidWarsContractGenerator{
    // Static logo path (can be updated as needed)
    private static LOGO_PATH = '/assets/hitter.png';
  
    /**
     * Format amount in Indian Rupee with precise number handling
     * @param amount Numeric amount to format
     * @returns Formatted rupee string
     */
    private static formatIndianRupee(amount: number | string): string {
        // Ensure amount is a number, default to 0 if invalid
        const numAmount = typeof amount === 'string' 
          ? parseFloat(amount.replace(/[^0-9.-]/g, '')) 
          : amount;
        
        const sanitizedAmount = isNaN(numAmount) ? 0 : numAmount;
    
        return `INR ${sanitizedAmount.toLocaleString('en-IN', { 
          minimumFractionDigits: 2, 
          maximumFractionDigits: 2 
        })}`;
      }
  
    /**
     * Safely get string value or default
     * @param value Input value
     * @param defaultValue Default value if input is invalid
     * @returns Processed string value
     */
    private static safeStringValue(value: any, defaultValue: string = 'Not Found'): string {
      return value ? String(value) : defaultValue;
    }
  
    /**
     * Generate and download contract PDF
     * @param player Player details
     * @param contract Contract details
     * @returns Promise<void>
     */
    static async generateContractPDF(player: Player, contract: Contract): Promise<void> {
      // Create new PDF document
      const doc = new jsPDF();
      const pageWidth = doc.internal.pageSize.getWidth();
      const marginLeft = 15;
      let currentY = 20;
  
      // Add Logo
    //   try {
    //     const logoWidth = 60; // Increased width
    //     const logoHeight = 30; // Proportional height

    //     doc.addImage(this.LOGO_PATH, 'PNG', 8, 8, logoWidth, logoHeight);
    //   } catch (error) {
    //     console.warn('Logo could not be added:', error);
    //   }
  
      // Corporate Header
      doc.setFillColor(33, 150, 243); // Material Blue
      doc.rect(0, 0, pageWidth, 25, 'F');
      doc.setTextColor(255, 255, 255);
      doc.setFont('Helvetica', 'bold');
      doc.setFontSize(18);
      doc.text('Bid Wars | Player Auction Contract', pageWidth / 2, 25, { align: 'center' });
  
      // Player Details Section
      currentY = 50;
      doc.setTextColor(0, 0, 0);
      doc.setFont('Helvetica', 'bold');
      doc.setFontSize(14);
      doc.text('Player Details', marginLeft, currentY);
      currentY += 10;
  
      // Calculate total compensation
      const totalCompensation = (contract.salary || 0) + (contract.bonuses || 0);
  
      // Use autoTable for structured player details
      (doc as any).autoTable({
        startY: currentY,
        head: [['Attribute', 'Details']],
        body: [
          ['Player ID', this.safeStringValue(player.playerId)],
          ['Name', this.safeStringValue(player.name)],
          ['Sport', this.safeStringValue(player.sport)],
          ['Age', this.safeStringValue(player.age)],
          ['Country', this.safeStringValue(player.country)],
          ['Position', this.safeStringValue(player.position)],
          ['Status', this.safeStringValue(player.status)]
        ],
        theme: 'striped',
        headStyles: { fillColor: [33, 150, 243] },
        columnStyles: { 0: { fontStyle: 'bold' } }
      });
  
      // Financial Terms
      currentY = (doc as any).lastAutoTable.finalY + 15;
      doc.setFont('Helvetica', 'bold');
      doc.setFontSize(14);
      doc.text('Financial Terms', marginLeft, currentY);
      currentY += 10;
  
      (doc as any).autoTable({
        startY: currentY,
        head: [['Component', 'Amount']],
        body: [
          ['Base Price', this.formatIndianRupee(player.basePrice || 0)],
          ['Base Salary', this.formatIndianRupee(contract.salary || 0)],
          ['Performance Bonuses', this.formatIndianRupee(contract.bonuses || 0)],
          ['Total Compensation', this.formatIndianRupee(totalCompensation)]
        ],
        theme: 'striped',
        headStyles: { fillColor: [33, 150, 243] },
        columnStyles: { 1: { halign: 'right' } }
      });
  
      // Contract Period
      currentY = (doc as any).lastAutoTable.finalY + 15;
      doc.setFont('Helvetica', 'bold');
      doc.setFontSize(14);
      doc.text('Contract Period', marginLeft, currentY);
      currentY += 10;
  
      (doc as any).autoTable({
        startY: currentY,
        head: [['Term', 'Date']],
        body: [
          ['Contract ID', this.safeStringValue(contract.contractId)],
          ['Start Date', contract.startDate ? contract.startDate.toLocaleString() : 'Not Found'],
          ['End Date', contract.endDate ? contract.endDate.toLocaleString() : 'Not Found']
        ],
        theme: 'striped',
        headStyles: { fillColor: [33, 150, 243] }
      });
  
      // Additional Details
      if (contract.details) {
        currentY = (doc as any).lastAutoTable.finalY + 15;
        doc.setFont('Helvetica', 'italic');
        doc.setFontSize(10);
        doc.text('Additional Contract Details:', marginLeft, currentY);
        currentY += 8;
        doc.text(this.safeStringValue(contract.details), marginLeft, currentY, { 
          maxWidth: pageWidth - marginLeft * 2 
        });
      }
  
      // Signature Lines
      currentY += 50;
      doc.setFont('Helvetica', 'normal');
      doc.text('Player Signature: _______________________', marginLeft, currentY);
      doc.text('Authorized Signature: ___________________', pageWidth / 2, currentY);
  
      // Footer
      doc.setFont('Helvetica', 'bold');
      doc.setFontSize(10);
      doc.text('© 2024 Bid Wars Auction Ltd.', pageWidth / 2, 285, { align: 'center' });
  
      // Save the PDF
      doc.save(`${this.safeStringValue(player.name)}_BidWars_Contract.pdf`);
    }
  }

